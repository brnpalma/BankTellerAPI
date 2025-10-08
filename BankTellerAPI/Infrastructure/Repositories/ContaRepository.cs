using BankTellerAPI.Application.Interfaces;
using BankTellerAPI.Domain.Entities;
using BankTellerAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BankTellerAPI.Infrastructure.Repositories
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
    }
}