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
    public class UsersController : ControllerBase 
    {
        private readonly ApiContext _context;
        public UsersController(ApiContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]User user)
        {
            var userTemp = await Task.FromResult(_context
                                                 .Users
                                                 .SingleOrDefault(x => x.Email == user.Email && x.Password == user.Password));

            if (userTemp != null)
            {
                var token = TokenService.GenerateToken(user);
                return Ok(new { user, token });
            }
            else
            {
                return NotFound("User not found.");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return Ok(await Task.FromResult(_context.Users.ToList()));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Show(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("User not found.");
            }
        }
    }

}