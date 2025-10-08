using BankTellerAPI.Domain.Entities;

namespace BankTellerAPI.Application.Interfaces
{
    public interface IContaRepository
    {
        Task<Conta?> ObterPorDocumentoAsync(string documento);
        Task<IEnumerable<Conta?>> ObterPorNomeAsync(string nomeCliente);
        Task CriarAsync(Conta conta);
        Task AtualizarAsync(Conta conta);
    }
}
