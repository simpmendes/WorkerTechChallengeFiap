using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TechChallengeFiap.Domain.Entities
{
    public class Acao:Entity
    {
        public string  Simbolo { get; set; }
        public decimal ValorAcao { get; set; }
        public DateTime DataValor { get; set; }
        public ICollection<PedidoAcao> PedidosAcao { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
