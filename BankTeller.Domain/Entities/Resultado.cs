using System.ComponentModel.DataAnnotations;

namespace BankTeller.Domain.Entities
{
    public class Resultado<T>
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public string? Erro { get; set; }
        public T? Value { get; set; }
    }
}
