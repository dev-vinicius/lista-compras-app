using ListaCompras.Server.Database;
using ListaCompras.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ListaCompras.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ItensController : ControllerBase
    {
        private readonly ApiContext _context;
        public ItensController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? list_id)
        {
            try
            {
                if (list_id.HasValue)
                {
                    var itens = await Task.FromResult(_context.Itens.Where(x => x.ListId == list_id).ToList());
                    return Ok(itens);
                }
                else
                {
                    return BadRequest(new { error = "A Lista não foi informada" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
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
                    return NotFound(new { error = "Item não encontrado" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Store(Item item)
        {
            try
            {
                if (item.IsValid)
                {
                    item.CreatedAt = DateTime.Now;
                    item.UpdatedAt = DateTime.Now;
                    await _context.Itens.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return Ok(item);
                }
                else
                {
                    return BadRequest(new { error = item.ValidationMessagesString() });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Item item)
        {
            try
            {
                if (item.IsValid)
                {
                    var itemTemp = await _context.Itens.FindAsync(id);
                    if (itemTemp != null)
                    {
                        itemTemp.Description = item.Description;
                        itemTemp.Note = item.Note;
                        itemTemp.Quantity = item.Quantity;
                        itemTemp.Price = item.Price;
                        itemTemp.Active = item.Active;
                        itemTemp.UpdatedAt = DateTime.Now;
                        await _context.SaveChangesAsync();
                        return Ok(itemTemp);
                    }
                    else
                    {
                        return BadRequest(new { error = "Item não encontrado" });
                    }
                }
                else
                {
                    return BadRequest(new { error = item.ValidationMessagesString() });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await _context.Itens.FindAsync(id);
                if (item != null)
                {
                    _context.Itens.Remove(item);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound(new { error = "Item não encontrado" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

    }
}
