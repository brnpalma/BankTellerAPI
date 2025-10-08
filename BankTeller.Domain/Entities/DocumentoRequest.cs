using System.ComponentModel.DataAnnotations;

namespace BankTeller.Domain.Entities
{
    public class DocumentoRequest
    {
        [Required(ErrorMessage = "O documento do cliente é obrigatório.")]
        [RegularExpression(@"^(\d{11}|\d{14})$", ErrorMessage = "O documento deve conter 11 ou 14 dígitos numéricos.")]
        public string Documento { get; set; }
    }
}
