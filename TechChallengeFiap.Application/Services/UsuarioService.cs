using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFiap.Application.DTOs;
using TechChallengeFiap.Application.Helpers;
using TechChallengeFiap.Application.Interfaces;
using TechChallengeFiap.Domain.Entities;
using TechChallengeFiap.Domain.Interfaces;
using TechChallengeFiap.Infra.Data;

namespace TechChallengeFiap.Application.Services;

public class UsuarioService : IUsuarioService
{
    private IUsuarioRepository _usuarioRepository;


    public UsuarioService(IUsuarioRepository usuarioRepository)
    {

        _usuarioRepository = usuarioRepository;
    }
    public async Task<Usuario> UsuarioComConsultas(int id) => await _usuarioRepository.ObterComConsultas(id);

    public async Task<List<UsuarioViewModel>> ObterTodos()
    {
        var usuarios = await _usuarioRepository.ObterTodosUsuariosAsync();

        var usuariosViewModel = new List<UsuarioViewModel>();
        foreach (var usuario in usuarios)
        {
            var usuarioViewModel = new UsuarioViewModel(usuario.Nome, usuario.NomeUsuario);
            usuariosViewModel.Add(usuarioViewModel);
        }

        return usuariosViewModel;
    }


    public Usuario ObterPorId(int id) => _usuarioRepository.ObterPorId(id);
    public async Task<IActionResult> CriarUsuario(CadastrarUsuarioDTO usuarioDto)
    {
        var usuario = new Usuario()
        {
            Nome = usuarioDto.Nome,
            NomeUsuario = usuarioDto.NomeUsuario,
            Senha = usuarioDto.Senha,
        };
        _usuarioRepository.Cadastrar(new Usuario(usuario));
        return new ResultObject(HttpStatusCode.Created, usuario);
    }
    public async Task<IActionResult> AlterarUsuario(AlterarUsuarioDTO usuarioDto)
    {
        var usuario = _usuarioRepository.ObterPorId(usuarioDto.Id);

        usuario.Nome = usuarioDto.Nome;
        _usuarioRepository.Alterar(usuario);
        return new ResultObject(HttpStatusCode.OK, usuario);

    }
    public async Task<IActionResult> DeletarUsuario(int id)
    {
        _usuarioRepository.Deletar(id);
        return new ResultObject(HttpStatusCode.OK, $"Usuario {id} removido");
    }
}
