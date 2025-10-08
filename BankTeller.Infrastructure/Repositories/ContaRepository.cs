using BankTeller.Infrastructure.Context;
using BankTellerAPI.Application.Interfaces;
using BankTellerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankTeller.Infrastructure.Repositories
{
    public class ContaRepository(BancoContext context) : IContaRepository
    {
        private readonly BancoContext _context = context;

        public async Task<Conta?> ObterPorDocumentoAsync(string documento)
        {
            return await _context.Contas.FirstOrDefaultAsync(c => c.DocumentoCliente == documento);
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

        public async Task<IEnumerable<Conta?>> ObterPorNomeAsync(string nomeCliente)
        {
            return await _context.Contas
                .Where(c => c.NomeCliente != null && c.NomeCliente.Contains(nomeCliente))
                .ToListAsync();
        }
    }
}