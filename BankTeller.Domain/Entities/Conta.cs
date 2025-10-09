using System.ComponentModel.DataAnnotations;

namespace BankTeller.Domain.Entities
{
    public class Conta(string nome, string documento)
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        public string? Nome { get; set; } = nome;

        [Required(ErrorMessage = "O documento do cliente é obrigatório.")]
        public string? Documento { get; set; } = documento;

        public DateTime DataAbertura { get; set; } = DateTime.Now;
        public long SaldoAtual { get; set; } = 1000;
        public bool Ativa { get; set; } = true;
        public DateTime? DataDesativacao { get; set; }
        public string? UsuarioDesativacao { get; set; }

        public void Inativar(string usuario)
        {
            Ativa = false;
            DataDesativacao = DateTime.Now;
            UsuarioDesativacao = usuario;
        }

        public void Transferir(Conta contaDestino, decimal valor)
        {
            SaldoAtual -= (long)valor;
            contaDestino.SaldoAtual += (long)valor;
        }
    }
}
