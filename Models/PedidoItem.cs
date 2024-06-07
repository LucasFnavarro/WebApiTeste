namespace WebApi.Codie.Models
{
    public class PedidoItem
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }

        public Pedido? Pedido { get; set; }
        public Produto? Produto { get; set; }
    }
}
