using BankTeller.Infrastructure.Context;
using BankTeller.Infrastructure.Repositories;
using BankTeller.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankTeller.Tests.Infrastructure.Tests
{
    public class ContaRepositoryTests
    {
        private BancoContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BancoContext>()
                .UseInMemoryDatabase(databaseName: "BancoTestDb")
                .Options;
            return new BancoContext(options);
        }

        [Fact]
        public async Task ObterPorDocumentoAsync_RetornaConta_QuandoDocumentoExiste()
        {
            // Arrange
            var context = GetInMemoryContext();
            var conta = new Conta("João", "123456789") { Id = 1 };
            context.Contas.Add(conta);
            await context.SaveChangesAsync();
            var repo = new ContaRepository(context);

            // Act
            var result = await repo.ObterPorDocumentoAsync("123456789");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("João", result?.Nome);
            Assert.Equal("123456789", result?.Documento);
        }

        [Fact]
        public async Task ObterPorDocumentoAsync_RetornaNull_QuandoDocumentoExiste()
        {
            var context = GetInMemoryContext();
            var repo = new ContaRepository(context);

            var result = await repo.ObterPorDocumentoAsync("000000000");

            Assert.Null(result);
        }

        [Fact]
        public async Task CriarAsync_AdicionaContaNoBanco()
        {
            var context = GetInMemoryContext();
            var repo = new ContaRepository(context);
            var conta = new Conta("Maria", "987654321");

            await repo.CriarAsync(conta);

            var dbConta = await context.Contas.FirstOrDefaultAsync(c => c.Documento == "987654321");
            Assert.NotNull(dbConta);
            Assert.Equal("Maria", dbConta?.Nome);
        }

        [Fact]
        public async Task AtualizarAsync_AtualizaCotaNoBanco()
        {
            var context = GetInMemoryContext();
            var conta = new Conta("Carlos", "111222333") { Id = 99 };
            context.Contas.Add(conta);
            await context.SaveChangesAsync();
            var repo = new ContaRepository(context);

            conta.Nome = "Carlos Silva";
            await repo.AtualizarAsync(conta);

            var dbConta = await context.Contas.FirstOrDefaultAsync(c => c.Id == 99);
            Assert.Equal("Carlos Silva", dbConta?.Nome);
        }

        [Fact]
        public async Task ObterPorNomeAsync_RetornaContasQueNomeCorresponde()
        {
            var context = GetInMemoryContext();
            context.Contas.AddRange(
                new Conta("Ana", "222333444"),
                new Conta("Anabela", "333444555"),
                new Conta("Bruno", "444555666")
            );
            await context.SaveChangesAsync();
            var repo = new ContaRepository(context);

            var result = await repo.ObterPorNomeAsync("Ana");

            Assert.Equal(2, result.Count());
            Assert.All(result, c => Assert.Contains("Ana", c?.Nome));
        }

        [Fact]
        public async Task RegistrarLogsInativacaoAsync_AdicionaLogsNoBanco()
        {
            var context = GetInMemoryContext();
            var repo = new ContaRepository(context);
            var log = new LogInativacao("123456789", "admin") { Descricao = "Conta inativada" };

            await repo.RegistrarLogsInativacaoAsync(log);

            var dbLog = await context.LogsInativacao.FirstOrDefaultAsync(l => l.Documento == "123456789");
            Assert.NotNull(dbLog);
            Assert.Equal("admin", dbLog?.UsuarioDesativacao);
            Assert.Equal("Conta inativada", dbLog?.Descricao);
        }
    }
}
