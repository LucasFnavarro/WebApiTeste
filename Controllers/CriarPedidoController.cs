using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Codie.Database;
using WebApi.Codie.Models;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly ApiDbContext _dbContext;

    public PedidoController(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("criar-pedido")]
    [Authorize]
    public async Task<IActionResult> CriarPedido([FromBody] PedidoRequest request)
    {
        if (request.Produtos == null || request.Produtos.Count < 2)
        {
            return BadRequest("É necessário informar pelo menos 2 produtos.");
        }

        // Verificar se todos os produtos existem
        var produtosIds = request.Produtos.Select(p => p.ProdutoId).ToList();
        var produtosExistentes = await _dbContext.Produtos
            .Where(p => produtosIds.Contains(p.ProdutoId))
            .ToListAsync();

        if (produtosExistentes.Count != produtosIds.Count!)
        {
            return BadRequest("Um ou mais produtos não existem.");
        }

        // Obter o ID do usuário autenticado
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        // Criar o pedido
        var pedido = new Pedido
        {
            UsuarioId = usuarioId,
            DataPedido = DateTime.UtcNow,
            PedidoItems = request.Produtos.Select(p => new PedidoItem
            {
                ProdutoId = p.ProdutoId,
                Quantidade = p.Quantidade
            }).ToList()
        };

        _dbContext.Pedidos.Add(pedido);
        await _dbContext.SaveChangesAsync();

        return Ok(pedido);
    }
}