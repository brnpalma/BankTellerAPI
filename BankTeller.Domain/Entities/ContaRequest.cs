using System.ComponentModel.DataAnnotations;

namespace BankTeller.Domain.Entities
{
    public class ContaRequest : DocumentoRequest
    {
        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        public string Nome { get; set; }
    }
}
