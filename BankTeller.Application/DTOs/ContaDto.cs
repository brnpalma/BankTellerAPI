using System.ComponentModel.DataAnnotations;

namespace BankTeller.Domain.Entities
{
    public class ContaDto : DocumentoDto
    {
        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        public string Nome { get; set; }
    }
}
