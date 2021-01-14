using ComandaWeb.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComandaWeb.DAL.Comanda.Repositorio
{
    public class ItemRepositorio : Repositorio<Item>, IItemRepositorio
    {
        public ItemRepositorio(ComandaContext contexto) : base(contexto)
        {

        }
    }
}
