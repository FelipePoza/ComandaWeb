using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComandaWeb.DAL.Comanda.Repositorio;
using ComandaWeb.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComandaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IUnidadeTrabalho _unidadeTrabalho;

        public ItemController(IUnidadeTrabalho unidadeTrabalho)
        {
            _unidadeTrabalho = unidadeTrabalho;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var item = await _unidadeTrabalho.ItemRepositorio.Listar().ToListAsync();
            return Ok(item);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            var item = await _unidadeTrabalho.ItemRepositorio.ListarPorId(a => a.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] Item item)
        {
             _unidadeTrabalho.ItemRepositorio.Adicionar(item);
            await _unidadeTrabalho.Salvar();
            var url = Url.Action("Listar");
            return Created(url,item);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }
            _unidadeTrabalho.ItemRepositorio.Atualizar(item);
            await _unidadeTrabalho.Salvar();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var item = await _unidadeTrabalho.ItemRepositorio.ListarPorId(a => a.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            _unidadeTrabalho.ItemRepositorio.Remover(item);
            await _unidadeTrabalho.Salvar();
            return NoContent();
        }
    }
}
