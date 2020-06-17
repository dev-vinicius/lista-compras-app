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
    public class ListsController : ControllerBase
    {
        private readonly ApiContext _context;
        public ListsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await Task.FromResult(_context
                                                    .Users
                                                    .Where(x => x.Email == User.Identity.Name)
                                                    .FirstOrDefault());
                return await Task.FromResult(Ok(_context
                                                    .Lists
                                                    .Where(x => x.UserId == user.Id)
                                                    .ToList()));
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
                var list = await _context.Lists.FindAsync(id);
                if (list != null)
                {
                    return Ok(list);
                }
                else
                {
                    return NotFound(new { error = "Lista não encontrada" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Store(List list)
        {
            try
            {
                if (list.IsValid)
                {
                    list.CreatedAt = DateTime.Now;
                    list.UpdatedAt = DateTime.Now;
                    await _context.Lists.AddAsync(list);
                    await _context.SaveChangesAsync();
                    return Ok(list);
                }
                else
                {
                    return BadRequest(new { error = list.ValidationMessagesString() });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, List list)
        {
            try
            {
                if (list.IsValid)
                {
                    var listTemp = await _context.Lists.FindAsync(id);
                    if (listTemp != null)
                    {
                        listTemp.Title = list.Title;
                        listTemp.Subtitle = list.Subtitle;
                        listTemp.Active = list.Active;
                        listTemp.UpdatedAt = DateTime.Now;
                        await _context.SaveChangesAsync();
                        return Ok(listTemp);
                    }
                    else
                    {
                        return BadRequest(new { error = "Lista não encontrada" });
                    }
                }
                else
                {
                    return BadRequest(new { error = list.ValidationMessagesString() });
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
                var list = await _context.Lists.FindAsync(id);
                if (list != null)
                {
                    _context.Lists.Remove(list);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound(new { error = "Lista não encontrada" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}
