using BankTeller.Domain.Entities;

namespace BankTeller.Tests.Domain.Tests
{
    public class ContaTests
    {
        [Fact]
        public void Inativar_AlterarAtivaParaFalse_EConfiguraDataDesativacao()
        {
            // Arrange
            var conta = new Conta("Cliente", "123456789");

            // Act
            conta.Inativar("UsuarioMaster");

            // Assert
            Assert.False(conta.Ativa);
            Assert.NotNull(conta.DataDesativacao);
            Assert.True(conta.DataDesativacao.Value <= DateTime.Now);
        }

        [Fact]
        public void Transferir_DecrementarSaldoAtual_EAumentarSaldoAtualDestino()
        {
            // Arrange
            var contaOrigem = new Conta("Origem", "111111111");
            var contaDestino = new Conta("Destino", "222222222");
            contaOrigem.SaldoAtual = 2000;
            contaDestino.SaldoAtual = 500;
            decimal valorTransferencia = 300;

            // Act
            contaOrigem.Transferir(contaDestino, valorTransferencia);

            // Assert
            Assert.Equal(1700, contaOrigem.SaldoAtual);
            Assert.Equal(800, contaDestino.SaldoAtual);
        }
    }
}
