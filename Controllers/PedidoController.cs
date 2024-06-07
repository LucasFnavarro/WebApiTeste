using Microsoft.AspNetCore.Mvc;
using WebApi.Codie.Database;
using WebApi.Codie.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public ProdutoController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("cadastrar-produtos")]
        public async Task<IActionResult> CadastrarProduto([FromBody] Produto produto)
        {
            try
            {
                if (produto == null)
                {
                    return BadRequest("Produto inválido.");
                }

                _dbContext.Produtos.Add(produto);
                await _dbContext.SaveChangesAsync();

                return Ok("Produto cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao cadastrar o produto: {ex.Message}");
            }
        }
    }
}
