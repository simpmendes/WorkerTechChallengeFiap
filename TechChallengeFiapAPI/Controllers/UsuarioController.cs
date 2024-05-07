using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechChallengeFiap.Application.DTOs;
using TechChallengeFiap.Application.Interfaces;
using TechChallengeFiap.Application.Services;
using TechChallengeFiap.Domain.Entities;
using TechChallengeFiap.Domain.Enums;

namespace TechChallengeFiapAPI.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(ILogger<UsuarioController> logger,
                                 IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }



        [Authorize(Roles = $"{Permissoes.Administrador}")]
        [HttpGet()]
        public async Task<IActionResult> ObterTodosUsuarios()
        {
            try
            {
                _logger.LogInformation("Executando método ObterTodosUsuarios");

                return Ok(await _usuarioService.ObterTodos());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} - Exception Forçada: {ex.Message}");
                return BadRequest();
            }

        }

        [Authorize(Roles = $"{Permissoes.Administrador}")]
        [HttpGet("obter-por-usuario-id/{id}")]
        public IActionResult ObterPorUsuarioId(int id)
        {
            try
            {
                _logger.LogInformation("Executando método ObterPorUsuarioId");
                return Ok(_usuarioService.ObterPorId(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} - Exception Forçada: {ex.Message}");
                return BadRequest();
            }

        }

        [Authorize(Roles = $"{Permissoes.Administrador}")]
        [HttpPost()]
        public IActionResult CriarUsuario(CadastrarUsuarioDTO usuarioDto)
        {
            try
            {
                _logger.LogInformation("Executando método CriarUsuario");
                _usuarioService.CriarUsuario(usuarioDto);
                var mensagem = $"Usuário criado com sucesso! | Nome: {usuarioDto.Nome}";
                _logger.LogInformation("mensagem");
                return Ok(mensagem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} - Exception Forçada: {ex.Message}");
                return BadRequest();
            }
        }

        [HttpPut()]
        public IActionResult AlterarUsuario(AlterarUsuarioDTO usuarioDto)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var userPermissao = User.FindFirst(ClaimTypes.Role)?.Value;
            if (int.Parse(userId) == usuarioDto.Id || userPermissao == "Administrador")
            {
                try
                {
                    _logger.LogInformation("Executando método AlterarUsuario");
                    _usuarioService.AlterarUsuario(usuarioDto);
                    var mensagem = $"Usuário alterado com sucesso! | Nome: {usuarioDto.Nome}";
                    _logger.LogInformation(mensagem);
                    return Ok(mensagem);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"{DateTime.Now} - Exception Forçada: {ex.Message}");
                    return BadRequest();
                }
            }
            else
            {
                _logger.LogError("Não é possivel alterar outro usuário.");
                return BadRequest("Não é possivel alterar outro usuário.");
            }

        }

        [Authorize(Roles = $"{Permissoes.Administrador}")]
        [HttpDelete("{id}")]
        public IActionResult DeletarUsuario(int id)
        {
            try
            {
                _logger.LogInformation("Executando método DeletarUsuario");
                _usuarioService.DeletarUsuario(id);
                var mensagem = $"Usuário deletado com sucesso! | Id: {id}";
                _logger.LogInformation(mensagem);
                return Ok(mensagem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} - Exception Forçada: {ex.Message}");
                return BadRequest();
            }
        }
    }
}
