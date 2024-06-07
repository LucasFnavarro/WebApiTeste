using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Codie.Database;
using WebApi.Codie.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaUrlController : ControllerBase
    {
        private readonly ApiDbContext _dbcontext;

        public CategoriaUrlController(ApiDbContext dbContext)
        {

            _dbcontext = dbContext;
        }

        [HttpGet("via-url{url}")]
        public async Task<IActionResult> GetCategoriaUrlController(string url)
        {
            var categoria = await _dbcontext.Categorias
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(c => c.Url == url);


            if (categoria == null)
            {
                return NotFound(new { message = "Categoria não encontrada." });
            }


            if (!categoria.Ativo)
            {
                return BadRequest(new { message = "Está categoria não está disponivel" });
            }

            var categoriaDto = new Categoria
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Produtos = categoria.Produtos!.Select(p => new Produto
                {
                    ProdutoId = p.ProdutoId,
                    Nome = p.Nome,
                    Url = p.Url,
                    Quantidade = p.Quantidade,
                    Ativo = true,
                }).ToList()
            };

            return Ok(categoriaDto);

        }
    }
}
