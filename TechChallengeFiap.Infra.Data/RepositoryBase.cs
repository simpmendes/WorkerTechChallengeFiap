using TechChallengeFiap.Domain.Entities;
using TechChallengeFiap.Domain.Interfaces;
using TechChallengeFiap.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace TechChallengeFiap.Infra.Data
{
    public class RepositoryBase<T>: IRepositoryBase<T> where T : Entity
    {
        protected ConsultaAcoesDBContext _context;
        protected DbSet<T> _dbSet;
        public RepositoryBase(ConsultaAcoesDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Alterar(T entidade)
        {
            _dbSet.Update(entidade);
            _context.SaveChanges();
        }

        public void Cadastrar(T entidade)
        {
            _dbSet.Add(entidade);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            _dbSet.Remove(ObterPorId(id));
            _context.SaveChanges();
        }

        public T ObterPorId(int id)
        {
            return _dbSet.FirstOrDefault(t => t.Id == id);
        }

        public IList<T> ObterTodos()
        {
            return _dbSet.ToList();
        }
        public async Task<IList<T>> ObterTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
