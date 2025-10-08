namespace BankTellerAPI.Domain.Entities
{
    public class Conta(string nomeCliente, string documentoCliente)
    {
        public int Id { get; set; }
        public string? NomeCliente { get; set; } = nomeCliente;
        public string? DocumentoCliente { get; set; } = documentoCliente;
        public DateTime DataAbertura { get; set; } = DateTime.Now;
        public long SaldoAtual { get; set; } = 1000;
        public bool Ativa { get; set; } = true;
        public DateTime DataDesativacao { get; set; }
        public string? UsuarioDesativacao { get; set; }

        public void Inativar()
        {
            Ativa = false;
            DataDesativacao = DateTime.Now;
        }

        public void Transferir(Conta contaDestino, decimal valor)
        {
            if (!Ativa || !contaDestino.Ativa)
            {
                throw new InvalidOperationException("As duas contas precisam estar ativas para concluir a transferencia.");
            }

            if (SaldoAtual < valor)
            {
                throw new InvalidOperationException("O saldo da conta é insuficiente para realizar a transferencia.");
            }

            SaldoAtual -= (long)valor;
            contaDestino.SaldoAtual += (long)valor;
        }
    }
}
