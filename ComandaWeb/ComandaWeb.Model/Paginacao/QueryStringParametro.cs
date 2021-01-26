using System;
using System.Collections.Generic;
using System.Text;

namespace ComandaWeb.Model.Paginacao
{
    public class QueryStringParametro
    {
        const int tamanhoMaximoPagina = 50;
        public int NumeroPagina { get; set; } = 1;
        private int _tamanhoPagina = 10;

        public int TamanhoPagina
        {
            get
            {
                return _tamanhoPagina;
            }
            set
            {
                _tamanhoPagina = (value > tamanhoMaximoPagina) ? tamanhoMaximoPagina : value;
            }
        }
    }
}
