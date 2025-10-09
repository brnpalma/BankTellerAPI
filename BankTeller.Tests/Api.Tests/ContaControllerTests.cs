using BankTeller.API.Controllers;
using BankTeller.Application.Interfaces;
using BankTeller.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BankTeller.Tests.Api.Tests
{
    public class ContaControllerTests
    {
        private readonly Mock<IBankService> _bankServiceMock;
        private readonly ContaController _controller;

        public ContaControllerTests()
        {
            _bankServiceMock = new Mock<IBankService>();
            _controller = new ContaController(_bankServiceMock.Object);
        }

        [Fact]
        public async Task Cadastrar_RetornarOk_QuandoSucesso()
        {
            var request = new ContaDto { Nome = "Test", Documento = "12345678901" };
            var conta = new Conta("Test", "12345678901") { DataAbertura = System.DateTime.Now, SaldoAtual = 100 };
            _bankServiceMock.Setup(s => s.CadastrarConta(request))
                .ReturnsAsync(new Resultado<Conta> { Sucesso = true, Value = conta });

            var result = await _controller.Cadastrar(request);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ContaResponseDto>(okResult.Value);
            Assert.Equal("Test", response.Nome);
            Assert.Equal("12345678901", response.Documento);
        }

        [Fact]
        public async Task Cadastrar_RetornarBadRequest_AoFalhar()
        {
            var request = new ContaDto { Nome = "Test", Documento = "12345678901" };
            _bankServiceMock.Setup(s => s.CadastrarConta(request))
                .ReturnsAsync(new Resultado<Conta> { Sucesso = false, Erro = "Erro" });

            var result = await _controller.Cadastrar(request);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Contains("Erro", badRequest.Value.ToString());
        }

        [Fact]
        public async Task ConsultarPorDocumento_RetornarOk_QuandoSucesso()
        {
            var documento = "12345678901";
            var contas = new List<Conta> { new Conta("Test", documento) };
            _bankServiceMock.Setup(s => s.ConsultarContasPorDocumento(documento))
                .ReturnsAsync(new Resultado<Conta> { Sucesso = true, Value = contas[0] });

            var result = await _controller.ConsultarPorDocumento(documento);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task ConsultarPorDocumento_RetornarBadRequest_AoFalhar()
        {
            var documento = "12345678901";
            _bankServiceMock.Setup(s => s.ConsultarContasPorDocumento(documento))
                .ReturnsAsync(new Resultado<Conta> { Sucesso = false, Erro = "Erro" });

            var result = await _controller.ConsultarPorDocumento(documento);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Contains("Erro", badRequest.Value.ToString());
        }

        [Fact]
        public async Task ConsultarPorNome_RetornarOk_QuandoSucesso()
        {
            var nome = "Test";
            var contas = new List<Conta> { new Conta(nome, "12345678901") };
            _bankServiceMock.Setup(s => s.ConsultarContasPorNome(nome))
                .ReturnsAsync(new Resultado<IEnumerable<Conta>> { Sucesso = true, Value = contas });

            var result = await _controller.ConsultarPorNome(nome);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task ConsultarPorNome_RetornarBadRequest_AoFalhar()
        {
            var nome = "Test";
            _bankServiceMock.Setup(s => s.ConsultarContasPorNome(nome))
                .ReturnsAsync(new Resultado<IEnumerable<Conta>> { Sucesso = false, Erro = "Erro" });

            var result = await _controller.ConsultarPorNome(nome);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Contains("Erro", badRequest.Value.ToString());
        }

        [Fact]
        public async Task Inativar_RetornarOk_QuandoSucesso()
        {
            var request = new InativaDto { Documento = "12345678901", NomeUsuario = "admin" };
            _bankServiceMock.Setup(s => s.InativarConta(request))
                .ReturnsAsync(new Resultado<string> { Sucesso = true, Mensagem = "Conta inativada" });

            var result = await _controller.Inativar(request);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Contains("Conta inativada", okResult.Value.ToString());
        }

        [Fact]
        public async Task Inativar_RetornarBadRequest_AoFalhar()
        {
            var request = new InativaDto { Documento = "12345678901", NomeUsuario = "admin" };
            _bankServiceMock.Setup(s => s.InativarConta(request))
                .ReturnsAsync(new Resultado<string> { Sucesso = false, Erro = "Erro" });

            var result = await _controller.Inativar(request);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Contains("Erro", badRequest.Value.ToString());
        }

        [Fact]
        public async Task Transferir_RetornarOk_QuandoSucesso()
        {
            var request = new TransferenciaDto
            {
                DocumentoOrigem = "12345678901",
                DocumentoDestino = "10987654321",
                Valor = 50
            };
            _bankServiceMock.Setup(s => s.Transferir(request))
                .ReturnsAsync(new Resultado<string> { Sucesso = true, Mensagem = "Transferência realizada" });

            var result = await _controller.Transferir(request);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Contains("Transferência realizada", okResult.Value.ToString());
        }

        [Fact]
        public async Task Transferir_RetornarBadRequest_AoFalhar()
        {
            var request = new TransferenciaDto
            {
                DocumentoOrigem = "12345678901",
                DocumentoDestino = "10987654321",
                Valor = 50
            };
            _bankServiceMock.Setup(s => s.Transferir(request))
                .ReturnsAsync(new Resultado<string> { Sucesso = false, Erro = "Erro" });

            var result = await _controller.Transferir(request);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Contains("Erro", badRequest.Value.ToString());
        }
    }
}
