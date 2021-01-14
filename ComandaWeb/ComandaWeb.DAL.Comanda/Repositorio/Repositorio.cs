using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ComandaWeb.DAL.Comanda.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ComandaContext _contexto;
        public Repositorio(ComandaContext contexto)
        {
            _contexto = contexto;
        }

        public void Adicionar(T entidade)
        {
            _contexto.Set<T>().Add(entidade);
        }

        public void Atualizar(T entidade)
        {
            _contexto.Entry(entidade).State = EntityState.Modified;
            _contexto.Set<T>().Update(entidade);
        }

        public IQueryable<T> Listar()
        {
          return  _contexto.Set<T>().AsNoTracking();
        }

        public Task<T> ListarPorId(Expression<Func<T, bool>> predicado)
        {
            return _contexto.Set<T>().SingleOrDefaultAsync(predicado);
        }

        public void Remover(T entidade)
        {
            _contexto.Set<T>().Remove(entidade);
        }
    }
}
