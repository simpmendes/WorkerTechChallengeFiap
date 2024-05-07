using TechChallengeFiap.Domain.Entities;

namespace TechChallengeFiap.Domain.Interfaces
{
    public interface IPedidoAcaoRepository : IRepositoryBase<PedidoAcao>
    {
        public Task<string> AprovaPedido(PedidoAcao pedido);
    
        
    }
}
