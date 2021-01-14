using System;
using System.Collections.Generic;
using System.Text;

namespace ComandaWeb.Model
{
    public static class ComandaExtensions
    {
        public static ComandaApi ToApi(this Comanda comanda)
        {
            return new ComandaApi
            {
                Id = comanda.Id,
                Codigo = comanda.Codigo
            };
        }

        public static Comanda ToModel(this ComandaApi comanda)
        {
            return new Comanda
            {
                Id = comanda.Id,
                Codigo = comanda.Codigo
            };
        }
    }
}
