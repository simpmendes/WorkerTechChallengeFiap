using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechChallengeFiap.Application.DTOs;
using TechChallengeFiap.Application.Interfaces;
using TechChallengeFiap.Domain.Entities;
using TechChallengeFiap.Domain.Interfaces;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using TechChallengeFiap.Application.Helpers;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace TechChallengeFiap.Application.Services
{
    public class CotacoesAcoesService : ICotacoesAcoesService
    {
        private readonly ILogger<CotacoesAcoesService> _logger;
        private readonly IApiExternaFinanceIntegration _apiExternaFinanceIntegration;
        private IConsultaAcoesRepository _consultaAcoesRepository;
        private IPedidoAcaoRepository _pedidoAcaoRepository;
        private IAcaoRepository _acaoRepository;
        private IAcoesUsuarioRepository _acoesUsuarioRepository;
        private IUsuarioRepository _usuarioRepository;

        public CotacoesAcoesService(ILogger<CotacoesAcoesService> logger,
                                    IApiExternaFinanceIntegration apiExternaFinanceIntegration,
                                    IConsultaAcoesRepository consultaAcoesRepository,
                                    IUsuarioRepository usuarioRepository,
                                    IPedidoAcaoRepository pedidoAcaoRepository,
                                    IAcaoRepository acaoRepository,
                                    IAcoesUsuarioRepository acoesUsuarioRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _apiExternaFinanceIntegration = apiExternaFinanceIntegration;
            _consultaAcoesRepository = consultaAcoesRepository;
            _usuarioRepository = usuarioRepository;
            _pedidoAcaoRepository = pedidoAcaoRepository;
            _acaoRepository = acaoRepository;
            _acoesUsuarioRepository = acoesUsuarioRepository;
        }
        public async Task<string> GetCotacao(string symbol, int idUsuario)
        {
            try
            {
                CadastrarConsulta(symbol, idUsuario);

                return await _apiExternaFinanceIntegration.GetCotacaoBySimbol(symbol);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao buscar cotação da ação {symbol}: {ex.Message}");
                throw;
            }
        }



        public async Task<string> GetTop10SubidasEDecidas(int idUsuario)
        {
            try
            {
                CadastrarConsulta("Top10", idUsuario);
                return await _apiExternaFinanceIntegration.GetTopGainerAndLosers();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao buscar as 10 ações que mais subiram no dia: {ex.Message}");
                throw;
            }
        }

        public async Task<string> CadastraAcao(Acao acao)
        {
            Acao acao1 = _acaoRepository.ObterPorSimbolo(acao.Simbolo);
            if (acao1 == null)
            {
                _acaoRepository.Cadastrar(acao);
                return $"Ação {acao.Simbolo} Cadastrada";
            }
            else
            {
                return $"Ação {acao.Simbolo} Ja cadastrada";
            }

        }

        public async Task<string> EfetuaPedido(PedidoAcaoDTO dto)
        {
            var acao = _acaoRepository.ObterPorSimbolo(dto.Simbolo);
            if (acao == null)
            {
                JObject acaoObj = JObject.Parse(await _apiExternaFinanceIntegration.GetCotacaoBySimbol(dto.Simbolo));
                if (!string.IsNullOrEmpty((string)acaoObj["Meta Data"]["2. Symbol"]))
                {
                    var data = (string)acaoObj["Meta Data"]["3. Last Refreshed"];
                    var valor = (decimal)acaoObj["Time Series (Daily)"][data]["4. close"];
                    acao = new Acao{ Simbolo= (string)acaoObj["Meta Data"]["2. Symbol"]
                                    ,ValorAcao= valor
                                    ,DataValor=DateTime.Parse(data)};

                    
                    _acaoRepository.Cadastrar(acao);
                }
            }
            PedidoAcao pedido = new PedidoAcao
            {   
                Id_Usuario = dto.Id_Usuario,
                Id_Acao = acao.Id,
                qtPedido = dto.qtPedido,
                ValorAcao = acao.ValorAcao,
                dtPedido = dto.dtPedido,
                dtAprovacao = null,

            };
            _pedidoAcaoRepository.Cadastrar(pedido);
            pedido.Acao = null;
            string mensagem = JsonConvert.SerializeObject(pedido);
            ServiceBusProdutor.EnviaMensagem(mensagem);

            return "Pedido Efetuado Pendente de Autorização";
        }

        private void CadastrarConsulta(string symbol, int idUsuario)
        {
            var usuarioCadastrado = _usuarioRepository.ObterPorId(idUsuario);
            var consulta = new ConsultaAcoes(symbol, usuarioCadastrado);
            _consultaAcoesRepository.Cadastrar(consulta);
        }
    }
}
