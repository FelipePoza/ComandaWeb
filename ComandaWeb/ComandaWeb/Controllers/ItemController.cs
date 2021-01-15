using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComandaWeb.DAL.Comanda.Repositorio;
using ComandaWeb.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ComandaWeb.Controllers
{
    //[Authorize(AuthenticationSchemes ="Bearer")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IUnidadeTrabalho _unidadeTrabalho;
        private readonly ILogger _log;

        public ItemController(IUnidadeTrabalho unidadeTrabalho,ILogger<ItemController> log)
        {
            _unidadeTrabalho = unidadeTrabalho;
            _log = log;
        }

        /// <summary>
        /// Listar Itens
        /// </summary>
        /// <returns>Json contendo lista de itens</returns>
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var item = await _unidadeTrabalho.ItemRepositorio.Listar().ToListAsync();
            return Ok(item);
        }

        /// <summary>
        /// Listar Item por ID
        /// </summary>
        /// <param name="id">Identificador do Item</param>
        /// <returns>Json contendo item referente ao id apresentado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ListarPorId(int id)
        {
            var item = await _unidadeTrabalho.ItemRepositorio.ListarPorId(a => a.Id == id);
            if (item == null)
            {
                _log.LogInformation("Item não localizado");
                return NotFound();
            }
            return Ok(item);
        }

        /// <summary>
        /// Inserir um novo item
        /// </summary>
        /// <param name="item">json contendo informações sobre o item</param>
        /// <returns>Retorna item cadastrado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Inserir([FromBody] Item item)
        {
             _unidadeTrabalho.ItemRepositorio.Adicionar(item);
            await _unidadeTrabalho.Salvar();
            var url = Url.Action("Listar");
            return Created(url,item);
        }

        /// <summary>
        /// Alterar informações do item
        /// </summary>
        /// <param name="id">Identificador do item</param>
        /// <param name="item">Json contendo dados do item</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Item item)
        {
            if (id != item.Id)
            {
                _log.LogInformation("Item não localizado para atualizacao");
                return NotFound();
            }
            _unidadeTrabalho.ItemRepositorio.Atualizar(item);
            await _unidadeTrabalho.Salvar();
            return Ok();
        }

        /// <summary>
        /// Remover item 
        /// </summary>
        /// <param name="id">Identificador do item</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Remover(int id)
        {
            var item = await _unidadeTrabalho.ItemRepositorio.ListarPorId(a => a.Id == id);
            if (item == null)
            {
                _log.LogInformation("Item não localizado para remoção");
                return NotFound();
            }
            _unidadeTrabalho.ItemRepositorio.Remover(item);
            await _unidadeTrabalho.Salvar();
            return NoContent();
        }
    }
}
