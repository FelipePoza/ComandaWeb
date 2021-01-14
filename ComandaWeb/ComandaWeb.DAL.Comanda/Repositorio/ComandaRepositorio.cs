using System;
using System.Collections.Generic;
using System.Text;

namespace ComandaWeb.DAL.Comanda.Repositorio
{
    public class ComandaRepositorio :Repositorio<ComandaWeb.Model.Comanda>,IComandaRepositorio
    {
        public ComandaRepositorio(ComandaContext contexto):base(contexto)
        {
            
        }

    }
}
