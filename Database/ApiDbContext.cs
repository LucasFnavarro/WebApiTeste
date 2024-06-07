using Microsoft.EntityFrameworkCore;
using WebApi.Codie.Models;

namespace WebApi.Codie.Database
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItems { get; set; }
        
        /*
        public void CriarCategoriasDeTeste()
        {
            if (!Categorias.Any())
            {
                var categoria = new Categoria
                {
                    Nome = "Categoria Celulares",
                    Url = "categoria-celulares",
                    Ativo = true,
                    Produtos = new List<Produto>
                    {
                        new Produto { Nome = "Iphone 14", Url = "iphone-14", Quantidade = 10, Ativo = true, Excluido = false },
                        new Produto { Nome = "Iphone 15", Url = "iphone-15", Quantidade = 5, Ativo = true, Excluido = false },
                        new Produto { Nome = "Iphone X", Url = "iphone-x", Quantidade = 2, Ativo = true, Excluido = false }
                    }
                };
                Categorias.Add(categoria);
                SaveChanges();
            }
        }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>()
           .HasMany(p => p.PedidoItems)
           .WithOne(pi => pi.Pedido)
           .HasForeignKey(pi => pi.PedidoId);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId);
        }
    }
}
