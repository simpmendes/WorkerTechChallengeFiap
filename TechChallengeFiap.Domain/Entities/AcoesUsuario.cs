using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallengeFiap.Domain.Entities
{
    public class AcoesUsuario:Entity
    {
        public string? Acao { get; set; }
        public int Id_Usuario { get; set; }
        public decimal qtTotal { get; set; }
    }
}
