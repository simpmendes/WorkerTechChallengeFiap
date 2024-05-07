using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFiap.Application.DTOs;
using TechChallengeFiap.Domain.Entities;

namespace TechChallengeFiap.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> UsuarioComConsultas(int id);
        Task<List<UsuarioViewModel>> ObterTodos();
        Usuario ObterPorId(int id);
        Task<IActionResult> CriarUsuario(CadastrarUsuarioDTO usuarioDto);
        Task<IActionResult> AlterarUsuario(AlterarUsuarioDTO usuarioDto);
        Task<IActionResult> DeletarUsuario(int id);

    }
}