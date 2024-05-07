using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallengeFiap.Domain.Interfaces
{
    public interface IApiExternaFinanceIntegration
    {
        Task<string> GetCotacaoBySimbol(string simbol);
        Task<string> GetTopGainerAndLosers();
    }
}
