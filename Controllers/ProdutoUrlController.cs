using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Codie.Database;
using WebApi.Codie.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoUrlController : ControllerBase
    {
        private readonly ApiDbContext _dbcontext;

        public ProdutoUrlController(ApiDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        [HttpGet("produto-via-{url}")]
        public async Task<IActionResult> GetProdutosPorUrl(string url)
        {
            // Busca um produto através da url informada.
            var produto = await _dbcontext.Produtos
                //.Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Url == url);


            // retorna uma msg caso o produto nao exista.
            if (produto == null)
            {
                return NotFound(new { message = "Produto não encontrado." });
            }

            // Vai verificar se o produto não está ativo.
            if (!produto.Ativo)
            {
                return BadRequest(new { message = "Produto não está disponvel" });
            }

            var produtoDto = new Produto
            {
                ProdutoId = produto.ProdutoId,
                Nome = produto.Nome,
                CategoriaId = produto.CategoriaId,
                Url = produto.Url,
                Quantidade = produto.Quantidade,
                Ativo = true,
            };
            return Ok(produtoDto);
        }
    }
}
