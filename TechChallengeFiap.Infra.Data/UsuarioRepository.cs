using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeFiap.Domain.Entities;
using TechChallengeFiap.Domain.Interfaces;
using TechChallengeFiap.Infra.Data.Context;

namespace TechChallengeFiap.Infra.Data
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly string _connectionString;
        public UsuarioRepository(ConsultaAcoesDBContext context,
            IConfiguration configuration) : base(context)
        {
            _connectionString = configuration.GetConnectionString("TechChallengeConection");
        }

        public async Task<Usuario> ObterComConsultas(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.ConsultasAcoes)
                .Where(u => u.Id == id)
                .Select(usuario =>
                new {
                    Usuario = usuario,
                    ConsultasAcoes = usuario.ConsultasAcoes.Select(consulta => new ConsultaAcoes(consulta)).ToList()
    
                })
                .FirstOrDefaultAsync();

            if (usuario is not null)
            {
                usuario.Usuario.ConsultasAcoes = usuario.ConsultasAcoes;
                return usuario.Usuario;
            }
            else
                return null; 
            
        }

        public async Task<Usuario> ObterPorNomeUsuarioESenha(string nomeUsuario, string senha)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(usuario =>
            usuario.NomeUsuario == nomeUsuario && usuario.Senha == senha);
        }
        public async Task<List<Usuario>> ObterTodosUsuariosAsync()
        {
            using var dbConnection = new SqlConnection(_connectionString);
            var query = "SELECT [Nome],[NomeUsuario] FROM [TechChallengeDB].[dbo].[Usuarios]";
            return  dbConnection.Query<Usuario>(query).ToList();
        }

    }
}
