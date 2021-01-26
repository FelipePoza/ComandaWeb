using ComandaWeb.Model.Paginacao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComandaWeb.DAL.Comanda.Repositorio
{
    public class ComandaRepositorio : Repositorio<Model.Comanda>, IComandaRepositorio
    {
        public ComandaRepositorio(ComandaContext contexto):base(contexto)
        {
            
        }

        public ListaPaginada<Model.Comanda> RetornarComanda(QueryStringParametro parametro)
        {
                return  ListaPaginada<Model.Comanda>.ParaListaPaginada(Listar()
                    .OrderBy(o => o.Codigo),
                    parametro.NumeroPagina,
                    parametro.TamanhoPagina);
        }

     
    }
}
