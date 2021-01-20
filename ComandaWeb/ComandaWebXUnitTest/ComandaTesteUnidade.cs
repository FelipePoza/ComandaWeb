using ComandaWeb;
using ComandaWeb.Controllers;
using ComandaWeb.DAL.Comanda;
using ComandaWeb.DAL.Comanda.Repositorio;
using ComandaWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ComandaWebXUnitTest
{
    public class ComandaTesteUnidade
    {
        private IUnidadeTrabalho _unidadeTrabalho;
        
        public static DbContextOptions<ComandaContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ComandaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static ComandaTesteUnidade()
        {
            dbContextOptions = new DbContextOptionsBuilder<ComandaContext>().UseSqlServer(connectionString).Options;
            
        }
        public ComandaTesteUnidade()
        {
            var context = new ComandaContext(dbContextOptions);
            _unidadeTrabalho = new UnidadeTrabalho(context);
        }

        [Fact]
        public async void RetornarComandaOk()
        {
            ILogger<ComandaController> log = null;
            var controller = new ComandaController(_unidadeTrabalho, log);
            var dados = await controller.Listar();
            var retornoOk = dados.Result as OkObjectResult;
            Assert.IsType<List<ComandaApi>>(retornoOk.Value);
        }


    }
}
