using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ComandaWeb.Model;
using ComandaWeb.Model.Paginacao;

namespace ComandaWeb.DAL.Comanda.Repositorio
{
   public interface IComandaRepositorio:IRepositorio<Model.Comanda>
   {
       ListaPaginada<Model.Comanda> RetornarComanda(QueryStringParametro parametro);

    }
}
