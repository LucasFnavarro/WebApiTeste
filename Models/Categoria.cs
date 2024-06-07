namespace WebApi.Codie.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Url { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }
        public ICollection<Produto>? Produtos { get; set; }
    }
}
