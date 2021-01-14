using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComandaWeb.DAL.Comanda.Repositorio;
using ComandaWeb.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ComandaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly IUnidadeTrabalho _unidadeTrabalho;
        
        public ComandaController(IUnidadeTrabalho unidadeTrabalho)
        {
            _unidadeTrabalho = unidadeTrabalho;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var comanda = await _unidadeTrabalho.ComandaRepositorio.Listar().Select(l=>l.ToApi()).ToListAsync();
            return Ok(comanda);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            var comanda = await _unidadeTrabalho.ComandaRepositorio.ListarPorId(a=>a.Id == id);
            if (comanda == null)
            {
              return NotFound();
            }
            return Ok(comanda.ToApi());
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] ComandaApi comanda)
        {
            _unidadeTrabalho.ComandaRepositorio.Adicionar(comanda.ToModel());
            await _unidadeTrabalho.Salvar();
            var url = Url.Action("Listar");
            return Created(url,comanda);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar(int? id,[FromBody] ComandaApi model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            _unidadeTrabalho.ComandaRepositorio.Atualizar(model.ToModel());
            await _unidadeTrabalho.Salvar();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var comanda = await _unidadeTrabalho.ComandaRepositorio.ListarPorId(a => a.Id == id);
                if (comanda == null)
                {
                    return NotFound();
                }
                 _unidadeTrabalho.ComandaRepositorio.Remover(comanda);
                await _unidadeTrabalho.Salvar();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest();
                //throw ;
            }
        }
    }
}
