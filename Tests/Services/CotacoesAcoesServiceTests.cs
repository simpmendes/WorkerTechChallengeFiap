using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Moq;
using TechChallengeFiap.Application.DTOs;
using TechChallengeFiap.Application.Interfaces;
using TechChallengeFiap.Application.Services;
using TechChallengeFiap.Domain.Entities;
using TechChallengeFiap.Domain.Interfaces;

namespace Tests.Services;

//[TestClass]
public sealed class CotacoesAcoesServiceTests
{
    private Mock<ILogger<CotacoesAcoesService>> _logger;
    private Mock<IApiExternaFinanceIntegration> _apiExternalFinanceIntegrationMock;
    private Mock<IConsultaAcoesRepository> consultaAcoesRepository;
    private Mock<IUsuarioRepository> usuarioRepository;
    private Mock<IPedidoAcaoRepository> PedidoAcaoRepository;
    private Mock<IAcaoRepository> acaoRepository;
    private Mock<IAcoesUsuarioRepository> acoesUaurioRepository;

    [TestInitialize]
    public void Setup()
    {
        _logger = new Mock<ILogger<CotacoesAcoesService>>();
        _apiExternalFinanceIntegrationMock = new Mock<IApiExternaFinanceIntegration>();
    }

    public CotacoesAcoesService CreateServiceInstance()
    {
        return new CotacoesAcoesService(
            logger: _logger.Object,
            apiExternaFinanceIntegration: _apiExternalFinanceIntegrationMock.Object
            ,consultaAcoesRepository.Object
            ,usuarioRepository.Object
            ,PedidoAcaoRepository.Object
            ,acaoRepository.Object
            , acoesUaurioRepository.Object
            );
    }

    //[TestMethod]
    public async Task GetCotacoes_ShoudReturnNotNull()
    {
        var symbol = "teste";
        var idUsuario = 1;

        var service = CreateServiceInstance();
        var response = service.GetCotacao(symbol,idUsuario);

        Assert.IsNotNull(response); 
    }

    //[TestMethod]
    public async Task GetTop10Cotacoes_ShoudReturnNotNull()
    {
        var cotacao = "teste cotacoes";
        var idUsuario = 1;

        var service = CreateServiceInstance();
        var response = service.GetTop10SubidasEDecidas(idUsuario);

        Assert.IsNotNull(response);
    }

}
