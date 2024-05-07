using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallengeFiap.Application.DTOs
{
    public class PedidoAcaoDTO
    {
        public string Simbolo { get; set; }
        public decimal qtPedido { get; set; }
        public DateTime dtPedido { get; set; } = DateTime.Now;
        
        [SwaggerIgnore]
        public int Id_Usuario { get; set; }
    }
}
