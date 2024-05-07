using TechChallengeFiap.Domain.Entities;

namespace TechChallengeFiap.Domain.Interfaces
{
    public interface IAcaoRepository : IRepositoryBase<Acao>
    {
        public Acao ObterPorSimbolo(string simbolo);

    }
}
