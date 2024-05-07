using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TechChallengeFiap.Domain.Entities
{
    public class PedidoAcao:Entity
    {
        public int Id_Usuario { get; set; }
        public int Id_Acao { get; set; }
        public decimal qtPedido { get; set; }
        public decimal ValorAcao { get; set; }
        public DateTime dtPedido { get; set; }
        public DateTime? dtAprovacao { get; set; }

        public Usuario usuario { get; set;}
        public Acao Acao { get; set;}


    }
}
