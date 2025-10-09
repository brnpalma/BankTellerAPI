using System.ComponentModel.DataAnnotations;

namespace BankTeller.Domain.Entities
{
    public class Transacao
    {
        public int Id { get; set; }
        public int IdContaOrigem { get; set; }
        public int IdContaDestino { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataTransacao { get; set; } = DateTime.Now;
    }
}
