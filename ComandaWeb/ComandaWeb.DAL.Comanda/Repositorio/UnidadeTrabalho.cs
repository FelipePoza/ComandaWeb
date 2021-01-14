using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComandaWeb.DAL.Comanda.Repositorio
{
    public class UnidadeTrabalho : IUnidadeTrabalho
    {
        public ComandaRepositorio _comandaRepo;
        public ComandaContext _contexto;

        public UnidadeTrabalho(ComandaContext contexto)
        {
            _contexto = contexto;
        }

        public IComandaRepositorio ComandaRepositorio
        {
            get
            {
                return _comandaRepo ?? new ComandaRepositorio(_contexto);
            }   
        }

        public async Task Salvar()
        {
            await _contexto.SaveChangesAsync();
        }

        public void Descartar()
        {
            _contexto.Dispose();
        }

    }
}
