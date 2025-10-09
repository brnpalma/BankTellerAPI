using System.ComponentModel.DataAnnotations;

namespace BankTeller.Domain.Entities
{
    public class InativaDto : DocumentoDto
    {
        [Required(ErrorMessage = "Seu nome de usuário é obrigatório.")]
        public string NomeUsuario { get; set; }
    }
}
