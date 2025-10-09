namespace BankTeller.Domain.Entities
{
    public class LogInativacao(string documento, string usuarioDesativacao)
    {
        public int Id { get; set; }
        public string? Documento { get; set; } = documento;
        public DateTime DataDesativacao { get; set; } = DateTime.Now;
        public string? UsuarioDesativacao { get; set; } = usuarioDesativacao;
        public string? Descricao { get; set; } = "Conta desativada.";
    }
}
