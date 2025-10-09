using BankTeller.Infrastructure.Context;
using BankTeller.Application.Interfaces;
using BankTeller.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankTeller.Infrastructure.Repositories
{
    public class ContaRepository(BancoContext context) : IContaRepository
    {
        private readonly BancoContext _context = context;

        public async Task<Conta?> ObterPorDocumentoAsync(string documento)
        {
            return await _context.Contas.FirstOrDefaultAsync(c => c.Documento == documento);
        }

        public async Task CriarAsync(Conta conta)
        {
            await _context.Contas.AddAsync(conta);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Conta conta)
        {
            _context.Contas.Update(conta);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Conta?>> ObterPorNomeAsync(string nome)
        {
            return await _context.Contas
                .Where(c => c.Nome != null && c.Nome.Contains(nome))
                .ToListAsync();
        }

        public async Task RegistrarInativaLogsAsync(InativaLog log)
        {
            await _context.InativaLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}