using BankTeller.Application.Interfaces;
using BankTeller.Application.Services;
using BankTeller.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BankTeller.Tests.Api.Tests
{
    public class BankServiceTests
    {
        private readonly Mock<IContaRepository> _repositoryMock;
        private readonly Mock<ILogger<BankService>> _loggerMock;
        private readonly BankService _service;

        public BankServiceTests()
        {
            _repositoryMock = new Mock<IContaRepository>();
            _loggerMock = new Mock<ILogger<BankService>>();
            _service = new BankService(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CadastrarConta_RetornarErro_QuandoContaJaExiste()
        {
            var request = new ContaDto { Nome = "Test", Documento = "12345678901" };
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync(request.Documento))
                .ReturnsAsync(new Conta(request.Nome, request.Documento));

            var result = await _service.CadastrarConta(request);

            Assert.False(result.Sucesso);
            Assert.Equal("Já existe uma conta cadastrada com este documento.", result.Erro);
        }

        [Fact]
        public async Task CadastrarConta_Corretamente_QuandoNaoExiste()
        {
            var request = new ContaDto { Nome = "Test", Documento = "12345678901" };
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync(request.Documento))
                .ReturnsAsync((Conta)null);

            var result = await _service.CadastrarConta(request);

            Assert.True(result.Sucesso);
            Assert.NotNull(result.Value);
            _repositoryMock.Verify(r => r.CriarAsync(It.IsAny<Conta>()), Times.Once);
        }

        [Fact]
        public async Task ConsultarContasPorDocumento_RetornarErro_QuandoNaoEncontraConta()
        {
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync("123"))
                .ReturnsAsync((Conta)null);

            var result = await _service.ConsultarContasPorDocumento("123");

            Assert.False(result.Sucesso);
            Assert.Equal("Nenhuma conta encontrada com o documento informado.", result.Erro);
        }

        [Fact]
        public async Task ConsultarContasPorDocumento_RetornarConta_QuandoEncontrar()
        {
            var conta = new Conta("Test", "123");
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync("123"))
                .ReturnsAsync(conta);

            var result = await _service.ConsultarContasPorDocumento("123");

            Assert.True(result.Sucesso);
            Assert.Equal(conta, result.Value);
        }

        [Fact]
        public async Task ConsultarContasPorNome_RetornarErro_QuandoNaoEncontraContas()
        {
            _repositoryMock.Setup(r => r.ObterPorNomeAsync("Test"))
                .ReturnsAsync(new List<Conta>());

            var result = await _service.ConsultarContasPorNome("Test");

            Assert.False(result.Sucesso);
            Assert.Equal("Nenhuma conta encontrada com o termo pesquisado.", result.Erro);
        }

        [Fact]
        public async Task ConsultarContasPorNome_RetornarContas_AoEncontrar()
        {
            var contas = new List<Conta> { new Conta("Test", "123") };
            _repositoryMock.Setup(r => r.ObterPorNomeAsync("Test"))
                .ReturnsAsync(contas);

            var result = await _service.ConsultarContasPorNome("Test");

            Assert.True(result.Sucesso);
            Assert.Equal(contas, result.Value);
        }

        [Fact]
        public async Task InativarConta_RetornarErro_QuandoNaoEncontrarConta()
        {
            var request = new InativaDto { Documento = "123", NomeUsuario = "user" };
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync(request.Documento))
                .ReturnsAsync((Conta)null);

            var result = await _service.InativarConta(request);

            Assert.False(result.Sucesso);
            Assert.Equal("Conta não encontrada para inativar.", result.Erro);
        }

        [Fact]
        public async Task InativarConta_RetornarErro_QuandoContaJaInativada()
        {
            var conta = new Conta("Test", "123") { Ativa = false };
            var request = new InativaDto { Documento = "123", NomeUsuario = "user" };
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync(request.Documento))
                .ReturnsAsync(conta);

            var result = await _service.InativarConta(request);

            Assert.False(result.Sucesso);
            Assert.Equal("A conta já está inativa.", result.Erro);
        }

        [Fact]
        public async Task InativarConta_InativarConta_QuandoAtiva()
        {
            var conta = new Conta("Test", "123");
            conta.Ativa = true;
            var request = new InativaDto { Documento = "123", NomeUsuario = "user" };
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync(request.Documento))
                .ReturnsAsync(conta);

            var result = await _service.InativarConta(request);

            Assert.True(result.Sucesso);
            Assert.Equal("A Conta foi alterada para inativa com sucesso.", result.Mensagem);
            _repositoryMock.Verify(r => r.AtualizarAsync(conta), Times.Once);
            _repositoryMock.Verify(r => r.RegistrarInativaLogsAsync(It.IsAny<InativaLog>()), Times.Once);
        }

        [Fact]
        public async Task Transferir_RetornarErro_QuandoContaOrigemOuDestinoNaoEncontrados()
        {
            var request = new TransferenciaDto { DocumentoOrigem = "1", DocumentoDestino = "2", Valor = 100 };
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync(request.DocumentoOrigem))
                .ReturnsAsync((Conta)null);
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync(request.DocumentoDestino))
                .ReturnsAsync((Conta)null);

            var result = await _service.Transferir(request);

            Assert.False(result.Sucesso);
            Assert.Equal("Conta origem ou destino não encontrada.", result.Erro);
        }

        [Fact]
        public async Task Transferir_RetornarErro_QuandoContaOrigemOuDestinoInativas()
        {
            var contaOrigem = new Conta("Origem", "1") { Ativa = false };
            var contaDestino = new Conta("Destino", "2") { Ativa = true };
            var request = new TransferenciaDto { DocumentoOrigem = "1", DocumentoDestino = "2", Valor = 100 };
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync(request.DocumentoOrigem))
                .ReturnsAsync(contaOrigem);
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync(request.DocumentoDestino))
                .ReturnsAsync(contaDestino);

            var result = await _service.Transferir(request);

            Assert.False(result.Sucesso);
            Assert.Equal("Conta origem ou destino inativa. Transferencias só podem ocorrer entre contas ativas.", result.Erro);
        }

        [Fact]
        public async Task Transferir_RetornarErro_QuandoSaldoInsuficiente()
        {
            var contaOrigem = new Conta("Origem", "1") { Ativa = true, SaldoAtual = 50 };
            var contaDestino = new Conta("Destino", "2") { Ativa = true };
            var request = new TransferenciaDto { DocumentoOrigem = "1", DocumentoDestino = "2", Valor = 100 };
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync(request.DocumentoOrigem))
                .ReturnsAsync(contaOrigem);
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync(request.DocumentoDestino))
                .ReturnsAsync(contaDestino);

            var result = await _service.Transferir(request);

            Assert.False(result.Sucesso);
            Assert.Equal("O saldo da conta é insuficiente para realizar a transferencia.", result.Erro);
        }

        [Fact]
        public async Task Transferir_Corretamente_QuandoValido()
        {
            var contaOrigem = new Conta("Origem", "1");
            var contaDestino = new Conta("Destino", "2");
            contaOrigem.Ativa = true;
            contaDestino.Ativa = true;
            contaOrigem.SaldoAtual = 1000;
            var request = new TransferenciaDto { DocumentoOrigem = "1", DocumentoDestino = "2", Valor = 100 };
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync(request.DocumentoOrigem))
                .ReturnsAsync(contaOrigem);
            _repositoryMock.Setup(r => r.ObterPorDocumentoAsync(request.DocumentoDestino))
                .ReturnsAsync(contaDestino);

            var result = await _service.Transferir(request);

            Assert.True(result.Sucesso);
            Assert.Equal("Transferência realizada com sucesso.", result.Mensagem);
            _repositoryMock.Verify(r => r.AtualizarAsync(contaDestino), Times.Once);
            _repositoryMock.Verify(r => r.AtualizarAsync(contaOrigem), Times.Once);
            contaOrigem.Transferir(contaDestino, request.Valor);
        }
    }
}