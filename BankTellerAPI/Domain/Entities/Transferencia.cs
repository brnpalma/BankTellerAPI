namespace BankTellerAPI.Domain.Entities
{
    public class Transferencia
    {
        public int Id { get; set; }
        public int IdContaOrigem { get; set; }
        public int IdContaDestino { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataTransferencia { get; set; }
    }
}
