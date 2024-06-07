namespace WebApi.Codie.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public Guid ChaveVerificacao { get; set; }
        public string? LastToken { get; set; }
        public bool IsVerificado { get; set; } = false;
        public bool Ativo { get; set; } = false;
        public bool Excluido { get; set; } = false;
    }
}
