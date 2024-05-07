using Moq;
using TechChallengeFiap.Application.DTOs;
using TechChallengeFiap.Application.Services;
using TechChallengeFiap.Domain.Entities;
using TechChallengeFiap.Domain.Interfaces;

namespace Tests.Services;

[TestClass]
public sealed class UsuarioServiceTests
{
    private Mock<IUsuarioRepository> _repositoryMock;

    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = new Mock<IUsuarioRepository>();
    }

    AlterarUsuarioDTO RetornaAlterarUsuarioDTO()
    {
        return new AlterarUsuarioDTO()
        {
            Id = 1,
            Nome = "alterado"
        };
    }

    Usuario RetornaUsuario()
    {
        return new Usuario()
        {
            Id = 1,
            Nome = "teste",
            NomeUsuario = "teste",
            Permissao = TechChallengeFiap.Domain.Enums.TipoPermissao.Administrador,
            Senha = "teste"
        };
    }

    CadastrarUsuarioDTO RetornaCadastrarUsuarioDTO()
    {
        return new CadastrarUsuarioDTO()
        {
            Nome = "teste",
            NomeUsuario = "teste",
            Senha = "teste"
        };
    }

    List<Usuario> RetornaListaUsuario()
    {
        return new List<Usuario>
        {
            new Usuario()
            {
                Id = 1,
                Nome = "teste",
                NomeUsuario = "teste",
                Permissao = TechChallengeFiap.Domain.Enums.TipoPermissao.Administrador,
                Senha = "teste"
            },
            new Usuario()
            {
                Id = 2,
                Nome = "testes",
                NomeUsuario = "teste2",
                Permissao = TechChallengeFiap.Domain.Enums.TipoPermissao.Usuario,
                Senha = "teste2"
            }
        };
    }

    public UsuarioService CreateServiceInstance()
    {
        return new UsuarioService(
            usuarioRepository: _repositoryMock.Object);
    }

    [TestMethod]
    public async Task ObterPorId_Should_Return_By_Id()
    {
        var user = RetornaUsuario();
        _repositoryMock.Setup(x => x.ObterPorId(1)).Returns(user);

        var service = CreateServiceInstance();
        var response = service.ObterPorId(1);

        Assert.IsNotNull(response);
        Assert.AreEqual(response, user);
    }

    [TestMethod]
    public async Task ObterTodos_Should_Return_Users()
    {
        List<Usuario> user = RetornaListaUsuario();

        _repositoryMock.Setup(x => x.ObterTodosUsuariosAsync()).ReturnsAsync(user);

        var service = CreateServiceInstance();
        var response = service.ObterTodos();

        Assert.IsNotNull(response);
        Assert.IsTrue(response.Result.Count > 0);
    }


    [TestMethod]
    public async Task UsuarioComConsultas_Should_Return_Users()
    {
        var user = new Usuario()
        {
            Id = 1,
            Nome = "teste",
            NomeUsuario = "teste",
            Permissao = TechChallengeFiap.Domain.Enums.TipoPermissao.Administrador,
            Senha = "teste"
        };

        _repositoryMock.Setup(x => x.ObterComConsultas(1)).ReturnsAsync(user);

        var service = CreateServiceInstance();
        var response = service.UsuarioComConsultas(1);

        Assert.IsNotNull(response);
    }


    [TestMethod]
    public async Task CriarUsuario_Should_Return_Ok()
    {
        var userDto = RetornaCadastrarUsuarioDTO();

        var user = RetornaUsuario();

        _repositoryMock.Setup(x => x.Cadastrar(user));

        var service = CreateServiceInstance();
        var response = service.CriarUsuario(userDto);

        Assert.IsNotNull(response);
        Assert.IsNull(response.Exception);
    }

    [TestMethod]
    public async Task Should_Delete_User()
    {
        var userDto = RetornaCadastrarUsuarioDTO();

        var user = RetornaUsuario();

        _repositoryMock.Setup(x => x.Deletar(user.Id));

        var service = CreateServiceInstance();
        var response = service.DeletarUsuario(user.Id);

        Assert.IsNotNull(response);
        Assert.IsNull(response.Exception);
    }

    [TestMethod]
    public async Task Should_Update_Usuario()
    {
        var userDto = RetornaAlterarUsuarioDTO();

        var user = RetornaUsuario();

        _repositoryMock.Setup(x => x.Alterar(user));

        var service = CreateServiceInstance();
        var response = service.AlterarUsuario(userDto);

        Assert.IsNotNull(response);
    }
}
