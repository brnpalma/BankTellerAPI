namespace BankTeller.Domain.Entities
{
    public class ContaResponseDto
    {
        public string Retorno { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string DataAbertura { get; set; }
        public long SaldoAtual { get; set; }
    }
}
