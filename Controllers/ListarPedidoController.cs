using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Codie.Database;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public PedidoController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpGet("listar-pedidos")]
        public async Task<IActionResult> ListarPedidos()
        {
            var pedidos = await _dbContext.Pedidos
                .Include(p => p.PedidoItems)
                .ToListAsync();

            var resultado = pedidos.Select(p => new
            {
                PedidoId = p.Id,
                Usuario = p.UsuarioId,
                Data = p.DataPedido,
                QuantidadeProdutos = p.PedidoItems.Count
            });

            return Ok(resultado);
        }
    }
}
