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
    public class PedidoAcaoRepository : RepositoryBase<PedidoAcao>, IPedidoAcaoRepository
    {
        private readonly string _connectionString;
        public PedidoAcaoRepository(ConsultaAcoesDBContext context,
            IConfiguration configuration) : base(context)
        {
            _connectionString = configuration.GetConnectionString("TechChallengeConection");
        }

        public async Task<string> AprovaPedido(PedidoAcao pedido)
        {
            pedido.dtAprovacao = DateTime.Now;
            _dbSet.Update(pedido);
            _context.SaveChanges();
            return "Pedido Aprovado com sucesso";
        }
    }
}
