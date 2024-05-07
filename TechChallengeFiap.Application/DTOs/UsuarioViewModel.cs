using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFiap.Domain.Entities;
using TechChallengeFiap.Domain.Enums;

namespace TechChallengeFiap.Application.DTOs
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {
                
        }
        public UsuarioViewModel(string nome, string nomeUsuario)
        {
            Nome = nome;
            NomeUsuario = nomeUsuario;
            
        }
        public string? Nome { get; set; }
        public string NomeUsuario { get; set; }
        //public TipoPermissao Permissao { get; set; }
    }
}
