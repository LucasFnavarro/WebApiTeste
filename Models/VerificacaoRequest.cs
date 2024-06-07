namespace WebApi.Codie.Models
{
    public class VerificacaoRequest
    {
       public string? Login { get; set; }
       public Guid ChaveVerificacao { get; set; }

    }
}
