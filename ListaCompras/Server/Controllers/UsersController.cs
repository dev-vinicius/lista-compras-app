using ListaCompras.Server.Database;
using ListaCompras.Server.Security;
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
    public class UsersController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly TokenManager _tokenManager;
        public UsersController(ApiContext context, TokenManager tokenManager)
        {
            _context = context;
            _tokenManager = tokenManager;
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
                if (userTemp.Password.Equals(user.Password))
                {
                    userTemp.Token = _tokenManager.GenerateToken(user);
                    userTemp.CleanPassword();
                    return Ok(userTemp);
                }
                else
                {
                    return NotFound(new { error = "Usuário e/ou Senha incorretos." });
                }
            }
            else
            {
                return NotFound(new { error = "Usuário e/ou Senha incorretos." });
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
                user.CleanPassword();
                return Ok(user);
            }
            else
            {
                return NotFound(new { error = "Usuário não encontrado" });
            }
        }

        [HttpPost]
        [AllowAnonymous]
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
                    user.Password = null;
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
