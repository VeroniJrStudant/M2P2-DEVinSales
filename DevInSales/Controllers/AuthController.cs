using DevInSales.Context;
using DevInSales.DTOs;
using DevInSales.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevInSales.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SqlContext _context;
        public AuthController(SqlContext context)
        {
            _context = context;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult> AuthenticateAsync([FromBody] UserLoginDto dto)
        {
            var user = await _context.User.Include(a=> a.Profile)
                .FirstOrDefaultAsync(a=> a.Email == dto.Email && a.Password ==dto.Password);
            if (user == null) return BadRequest(new { Message = "Email ou senha inválido." });
            var token = TokenUsers.GenerateToken(user);

            var result = new 
            {
                token,
                User = new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                }
            };
            return Ok(new { result });
        }

        [HttpGet("endpoint-aberto")]
        public ActionResult EndpointAberto() 
        {
            return Ok(new {Messege = "Bem vindo ao endpoint aberto! =)" });
        }

        [HttpGet("endpoint-usuario")]
        [Authorize(Roles = "Usuário, Gerente, Administrador")]
        public ActionResult EndpointUsuario()
        {
            return Ok(new { Messege = "Bem vindo ao endpoint do usuário! =)" });
        }

        [HttpGet("endipoint-gerente")]
        [Authorize(Roles = "Gerente, Administrador")]
        public ActionResult actionResult()
        {
            return Ok(new { Messege = "Bem vindo ao endpoint do gerente =)" });
        }

        [HttpGet("endpoint-admin")]
        [Authorize(Roles ="Administrador")]
        public ActionResult EndpointAdmin()
        {
            return Ok(new { Messege = "Bem vindo ao endpoint do admin =)" });
        }
    }
}
