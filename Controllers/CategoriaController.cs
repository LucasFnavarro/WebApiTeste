using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Codie.Database;
using WebApi.Codie.Models;

namespace WebApi.Codie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public CategoriaController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // Rota para verificar as categorias disponiveis e com produtos a cima de 0.
        [HttpGet("ativas-com-produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> ListarCategoriasAtivasComProdutos()
        {
            var categorias = await _dbContext.Categorias
                .Include(c => c.Produtos)
                .Where(c => c.Ativo && c.Produtos!.Any(p => p.Quantidade > 0 && p.Ativo))
                .ToListAsync();


            var categoriasDto = categorias.Select(c => new Categoria
            {
                Id = c.Id,
                Nome = c.Nome,
                Url = c.Url,
                Ativo = true,
                Produtos = c.Produtos!.Select(p => new Produto
                {
                    ProdutoId = p.ProdutoId,
                    Nome = p.Nome,
                    Url = p.Url,
                    Quantidade = p.Quantidade,
                    CategoriaId = p.CategoriaId,
                    Ativo = p.Ativo,
                }).ToList()
            }).ToList();


            if (categoriasDto == null)
            {
                return NotFound("Categoria não encontrada, ou é inexistente.");
            }
            return Ok(categoriasDto);
        }
    }
}
