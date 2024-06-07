using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Codie.Database;
using WebApi.Codie.Models;

namespace WebApi.Codie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UsuarioController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUser([FromBody] Usuario usuario)
        {
            if(await _context.Usuarios.AnyAsync(u => u.Login == usuario.Login))
            {
                return Conflict(new { message = "Este nome de login já existe." });
            }

            usuario.Ativo = true;
            usuario.Excluido = false;
            usuario.IsVerificado = false;
            usuario.ChaveVerificacao = Guid.NewGuid();

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = "O usuário foi cadastrado com sucesso." });
        }


        [HttpPost("verify-email-usuario")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerificacaoRequest request)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.Login == request.Login && u.ChaveVerificacao == request.ChaveVerificacao)
                .FirstOrDefaultAsync();

            if(usuario == null)
            {
                return NotFound(new { message = "Usuario não encontrado ou a chave de verificação está incorreta." });
            }

            usuario.IsVerificado = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "E-mail verificado com sucesso." });
        }
    }
}
