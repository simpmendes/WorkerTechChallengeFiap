using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallengeFiap.Domain.Entities
{
    public class ConsultaAcoes: Entity
    {
        public string Symbol { get; set; }
        public DateTime DataConsulta { get; set; }
        public int UsuarioId { get; set; } // Chave estrangeira para Usuario
        public Usuario Usuario { get; set; } // Propriedade de navegação para Usuario

        public ConsultaAcoes()
        {
            
        }
        public ConsultaAcoes(ConsultaAcoes consulta)
        {
            Symbol = consulta.Symbol;
            DataConsulta = consulta.DataConsulta;
            UsuarioId = consulta.UsuarioId;
        }
        public ConsultaAcoes(string symbol, Usuario usuario)
        {
            Symbol = symbol;
            DataConsulta = DateTime.Now;
            Usuario = usuario;
        }
    }
}
