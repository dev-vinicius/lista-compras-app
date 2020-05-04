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
        public async Task<IActionResult> Login(User user)
        {
            var userTemp = await Task.FromResult(_context
                                                 .Users
                                                 .SingleOrDefault(x => x.Email == user.Email));

            if (userTemp != null)
            {
                user.GenerateMD5Password();
                if (userTemp.Password.Equals(user.Password)){
                    var token = TokenService.GenerateToken(user);
                    return Ok(new { username = user.Email, access_token = token });
                }
                else
                {
                    return NotFound(new { error = "Usuário e/ou Senha incorretos."});
                }
            }
            else
            {
                return NotFound(new { error = "Usuário e/ou Senha incorretos."});
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Show()
        {
            var user = await Task.FromResult(_context.Users
                                                .Where(x => x.Email == User.Identity.Name)
                                                .ToList()
                                                .FirstOrDefault());
            if (user != null)
            {
                user.Password = null;
                return Ok(user);
            }
            else
            {
                return NotFound(new { error = "Usuário não encontrado"});
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Store(User user)
        {
            try
            {
                if (user.IsValid)
                {
                    user.GenerateMD5Password();
                    user.CreatedAt = DateTime.Now;
                    user.UpdatedAt = DateTime.Now;
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return Ok(user);
                }
                else
                {
                    return BadRequest(new { error = user.ValidationMessagesString() });
                }
            }
            catch (System.Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }

}