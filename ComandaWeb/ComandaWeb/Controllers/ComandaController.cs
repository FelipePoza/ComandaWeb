using System.Linq;
using System.Threading.Tasks;
using ComandaWeb.DAL.Comanda.Repositorio;
using ComandaWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace ComandaWeb.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly IUnidadeTrabalho _unidadeTrabalho;
        private readonly ILogger _log;

        public ComandaController(IUnidadeTrabalho unidadeTrabalho,ILogger<ComandaController> log)
        {
            _unidadeTrabalho = unidadeTrabalho;
            _log = log;
        }
        
        /// <summary>
        /// Listar Comandas cadastradas
        /// </summary>
        /// <returns>Retornar json contendo as comandas cadastradas.</returns>
        [HttpGet]
        public async Task<ActionResult<IList<ComandaApi>>> Listar()
        {
            var comanda = await _unidadeTrabalho.ComandaRepositorio.Listar().Select(l=>l.ToApi()).ToListAsync();
            return Ok(comanda);
        }


        /// <summary>
        /// Retornar comanda cadastrada por id
        /// </summary>
        /// <remarks>
        /// Exemplo de Requisição:
        ///
        ///     GET /Todo
        ///     {
        ///        "id": 1
        ///     }
        /// </remarks>
        /// <param name="id">Id da comanda cadastrada</param>
        /// <returns>Retornar json contendo a comanda cadastrada para o id informado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ComandaApi>> ListarPorId(int id)
        {
            var comanda = await _unidadeTrabalho.ComandaRepositorio.ListarPorId(a=>a.Id == id);
            if (comanda == null)
            {
                _log.LogInformation("Comanda não localizada");
                return NotFound();
            }
            return Ok(comanda.ToApi());
        }

        /// <summary>
        /// Inserir uma nova comanda na base de dados
        /// </summary>
        /// <remarks>
        /// Exemplo de Requisição:
        ///
        ///     POST /Todo
        ///     {
        ///        "codigo": 1
        ///     }
        /// </remarks>
        /// <param name="comanda">Json contendo informações da comanda</param>
        /// <returns>Retornar comanda cadastrada</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Inserir([FromBody] ComandaApi comanda)
        {
            var comandaModelo = comanda.ToModel();
            _unidadeTrabalho.ComandaRepositorio.Adicionar(comandaModelo);
            await _unidadeTrabalho.Salvar();
            _log.LogInformation("Comanda criada.");
            var url = Url.Action("ListarPorId", new { id = comandaModelo.Id });
            return new CreatedResult(url, comandaModelo);
        }

        /// <summary>
        /// Alterar dados da comanda
        /// </summary>
        /// <remarks>
        /// Exemplo de Requisição:
        ///
        ///     PUT /Todo
        ///     {
        ///        "codigo": 5
        ///     }
        /// </remarks>
        /// <param name="id">Identificador da comanda</param>
        /// <param name="model">Json contendo a comanda cadastrada</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Alterar(int? id,[FromBody] ComandaApi model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            _unidadeTrabalho.ComandaRepositorio.Atualizar(model.ToModel());
            await _unidadeTrabalho.Salvar();
            _log.LogInformation("Comanda atualzida.");
            return Ok();
        }

        /// <summary>
        /// Remover uma comanda
        /// </summary>
        /// /// <remarks>
        /// Exemplo de Requisição:
        ///
        ///     DELETE /Todo
        ///     {
        ///        "id": 5
        ///     }
        /// </remarks>
        /// <param name="id">Identificador da comanda cadastrada</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var comanda = await _unidadeTrabalho.ComandaRepositorio.ListarPorId(a => a.Id == id);
                if (comanda == null)
                {
                    _log.LogInformation("Comanda não localizada.");
                    return NotFound();
                }
                 _unidadeTrabalho.ComandaRepositorio.Remover(comanda);
                await _unidadeTrabalho.Salvar();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                _log.LogError("Erro ao remover Comanda.");
                return BadRequest();
                //throw ;
            }
        }
    }
}
