using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIListaCompras.Database;
using APIListaCompras.Models;
using APIListaCompras.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIListaCompras.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItensController : ControllerBase
    {
        private readonly ApiContext _context;
        public ItensController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Show(int id)
        {
            try
            {
                var item = await _context.Itens.FindAsync(id);
                if (item != null)
                {
                    return Ok(item);
                }
                else
                {
                    return NotFound(new {error = "Item não encontrado"});
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new {error = e.Message});
            }
        }

        [HttpPost]
        public async Task<IActionResult> Store(Item item)
        {

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Item item)
        {

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

        }
        
    }

}