using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ComandaWeb.DAL.Comanda.Repositorio
{
    public interface IRepositorio<T>
    {
        IQueryable<T> Listar();
        Task<T> ListarPorId(Expression<Func<T, bool>> predicado);
        void Adicionar(T entidade);
        void Atualizar(T entidade);
        void Remover(T entidade);
    }
}
