using TechChallengeFiap.Domain.Entities;

namespace TechChallengeFiap.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<Usuario> ObterComConsultas(int id);
        Task<Usuario> ObterPorNomeUsuarioESenha(string nomeUsuario, string senha);
        Task<List<Usuario>> ObterTodosUsuariosAsync();
    }
}
