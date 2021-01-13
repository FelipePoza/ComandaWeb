using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComandaWeb.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComandaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Inserir()
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Alterar(int? id,[FromBody] Comanda model)
        {
            if (id == null)
            {
                return NotFound();
            }//TryUpdateModelAsync

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            try
            {

            }
            catch (DbUpdateException ex)
            {
                return BadRequest();
                //throw ;
            }
            return Ok();
        }

    }
}
