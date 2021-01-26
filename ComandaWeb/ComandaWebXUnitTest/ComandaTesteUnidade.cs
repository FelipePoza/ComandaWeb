using ComandaWeb;
using ComandaWeb.Controllers;
using ComandaWeb.DAL.Comanda;
using ComandaWeb.DAL.Comanda.Repositorio;
using ComandaWeb.Model;
using ComandaWeb.Model.Paginacao;
using FluentAssertions;
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
        private ILogger<ComandaController> log;

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
            log = TesteUnidadeLog.Create<ComandaController>();
        }

    /*    [Fact]
        public async void RetornarComandaOk()
        {
            var parametro = new ComandaParametro
            {
                NumeroPagina = 0,
                TamanhoPagina = 10
            };

            var controller = new ComandaController(_unidadeTrabalho, log);
            var dados = await controller.Listar(parametro);
            var retornoOk = dados.Result as OkObjectResult;
            Assert.IsType<ActionResult<List<ComandaApi>>>(retornoOk.Value);
        }*/

        [Fact]
        public async void InserirDadosBase()
        {
            var controller = new ComandaController(_unidadeTrabalho, log);
            var comandaCriada = new ComandaApi() {Codigo = 36548};
            var data = await controller.Inserir(comandaCriada);
            Assert.IsType<CreatedResult>(data);
        }

        [Fact]
        public async void AlterarDadosBase()
        {
            var controller = new ComandaController(_unidadeTrabalho, log);
            var comandaId = 1004;
            var existeComanda = await controller.ListarPorId(comandaId);
            var result = existeComanda.Value.Should().BeAssignableTo<Comanda>().Subject;
            var comanda = new ComandaApi();
            comanda.Id = comandaId;
            comanda.Codigo = 1111;
            var update = await controller.Alterar(comandaId, comanda);
            Assert.IsType<OkResult>(update);
        }

        [Fact]
        public async void DeleteComandaBase()
        {
            var controller = new ComandaController(_unidadeTrabalho, log);
            var comandaId = 1012;
            var data = await controller.Remover(comandaId);
            Assert.IsType<NoContentResult>(data);
        }
    }
}
