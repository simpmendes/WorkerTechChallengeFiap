using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFiap.Domain.Entities;

namespace TechChallengeFiap.Domain.Interfaces
{
    public interface ITokenService
    {
        string GetToken(Usuario usuario);
    }
}
