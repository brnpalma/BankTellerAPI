namespace BankTellerAPI.Domain.Entities
{
    public class Conta
    {
        public int Id { get; set; }
        public string? NomeCliente { get; set; }
        public string? DocumentoCliente { get; set; }
        public DateTime DataAbertura { get; set; }
        public long SaldoAtual { get; set; }
        public bool Ativa { get; set; }
        public DateTime DataDesativacao { get; set; }
        public string? UsuarioDesativacao { get; set; }
    }
}
