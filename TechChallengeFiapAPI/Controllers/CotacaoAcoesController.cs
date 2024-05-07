using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechChallengeFiap.Application.DTOs;
using TechChallengeFiap.Application.Interfaces;
using TechChallengeFiap.Domain.Enums;

namespace TechChallengeFiap.Controllers
{
    [ApiController]
    [Tags("Ações")]
    [Route("api/Acoes")]
    public class CotacaoAcoesController : ControllerBase
    {
        private readonly ILogger<CotacaoAcoesController> _logger;
        private readonly ICotacoesAcoesService _cotacaoAcoesService;

        public CotacaoAcoesController(ILogger<CotacaoAcoesController> logger,
                                      ICotacoesAcoesService cotacoesAcoesService)
        {
            _logger = logger;
            _cotacaoAcoesService = cotacoesAcoesService;
        }
        [Authorize(Roles = $"{Permissoes.Administrador}, {Permissoes.Usuario}")]
        [HttpGet("{simbolo}")]
        public async Task<IActionResult> GetCotacao(string simbolo)
        {
            try
            {       
                var content = await _cotacaoAcoesService.GetCotacao(simbolo, GetUserId());
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao buscar cotação da ação {simbolo}: {ex.Message}");
                return StatusCode(500, "Erro interno ao processar a solicitação.");
            }
        }
        [Authorize(Roles = $"{Permissoes.Administrador}, {Permissoes.Usuario}")]
        [HttpGet("top10")]
        public async Task<IActionResult> GetTop10SubidasEDecidas()
        {
            try
            {
                var content = await _cotacaoAcoesService.GetTop10SubidasEDecidas(GetUserId());
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao buscar as 10 ações que mais subiram no dia: {ex.Message}");
                return StatusCode(500, "Erro interno ao processar a solicitação.");
            }
        }
        private int GetUserId()
        {
            var userValue = (HttpContext.User.Identity as ClaimsIdentity)?.FindFirst("Id").Value;
            int userId = Convert.ToInt32(userValue);
            return userId;
        }

        [Authorize(Roles = $"{Permissoes.Administrador}, {Permissoes.Usuario}")]
        [HttpPost("efetuarpedido")]
        public async Task<IActionResult> PostEfetuaPedido(PedidoAcaoDTO dto)
        {
            var userId = GetUserId();
            dto.Id_Usuario = userId;
            try
            {
                var content = await _cotacaoAcoesService.EfetuaPedido(dto);
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao Gravar o pedido: {ex.Message}");
                return StatusCode(500, "Erro interno ao processar a solicitação.");
            }
        }

        [Authorize(Roles = $"{Permissoes.Administrador}")]
        [HttpPost("aprovapedido")]
        public async Task<IActionResult> PutAprovaPedido(PedidoAcaoDTO dto)
        {   //todo criar o autorizador de pedidos
            var userId = GetUserId();
            dto.Id_Usuario = userId;
            try
            {
                var content = await _cotacaoAcoesService.EfetuaPedido(dto);
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao Gravar o pedido: {ex.Message}");
                return StatusCode(500, "Erro interno ao processar a solicitação.");
            }
        }

    }
}
