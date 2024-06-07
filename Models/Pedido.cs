namespace WebApi.Codie.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime? DataPedido { get; set; }

        public Usuario? Usuario { get; set; }
        public List<PedidoItem> PedidoItems { get; set; }
    }
}
