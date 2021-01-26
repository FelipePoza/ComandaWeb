using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComandaWeb.Model.Paginacao
{
    public class ListaPaginada<T>:List<T>
    {
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanhoPagina { get; set; }
        public int TotalRegistros { get; set; }

        public bool TemAnterior => PaginaAtual > 1;
        public bool TemProxima => PaginaAtual < TotalPaginas;

        public ListaPaginada(List<T> item, int total, int numeroPagina,int totalPagina)
        {
            TotalRegistros = total;
            TamanhoPagina = totalPagina;
            PaginaAtual = numeroPagina;
            TotalPaginas = (int)Math.Ceiling(total / (double)TamanhoPagina);
            AddRange(item);
        }

        public static ListaPaginada<T> ParaListaPaginada(IQueryable<T> dados, int numeroPagina, int tamanhoPagina)
        {
            var count = dados.Count();
            var items = dados.Skip((numeroPagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToList();
            return new ListaPaginada<T>(items, count, numeroPagina, tamanhoPagina);
        }
    }
}
