using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComandaWeb.DAL.Comanda.Repositorio
{
    public interface IUnidadeTrabalho
    {
        IComandaRepositorio ComandaRepositorio { get; }
        Task Salvar();
    }
}
