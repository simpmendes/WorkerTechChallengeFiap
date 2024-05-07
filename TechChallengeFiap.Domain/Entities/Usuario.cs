using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFiap.Domain.Enums;

namespace TechChallengeFiap.Domain.Entities
{
    public class Usuario: Entity
    {
        public string? Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public TipoPermissao Permissao { get; set; }
        public ICollection<ConsultaAcoes> ConsultasAcoes { get; set; }

        public ICollection<PedidoAcao> PedidosAcoes { get; set; }
        public ICollection<Acao> Acoes { get; set; }

        public Usuario()
        {
                
        }
        public Usuario(Usuario inputModel)
        {
            Nome = inputModel.Nome;
            NomeUsuario = inputModel.NomeUsuario;
            Senha = inputModel.Senha;
            Permissao = TipoPermissao.Usuario;
        }
        public Usuario(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
