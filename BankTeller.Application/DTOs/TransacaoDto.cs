using System.ComponentModel.DataAnnotations;

namespace BankTeller.Domain.Entities
{
    public class TransacaoDto
    {
        [Required(ErrorMessage = "O documento de origem é obrigatório.")]
        [RegularExpression(@"^(\d{11}|\d{14})$", ErrorMessage = "O documento deve conter 11 ou 14 dígitos numéricos.")]
        public string DocumentoOrigem { get; set; }

        [Required(ErrorMessage = "O documento de destino é obrigatório.")]
        [RegularExpression(@"^(\d{11}|\d{14})$", ErrorMessage = "O documento deve conter 11 ou 14 dígitos numéricos.")]
        public string DocumentoDestino { get; set; }

        [Required(ErrorMessage = "O valor da transferência é obrigatório.")]
        public decimal Valor { get; set; }
    }
}
