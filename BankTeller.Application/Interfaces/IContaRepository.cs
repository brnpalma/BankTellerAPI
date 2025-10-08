using BankTeller.Domain.Entities;

namespace BankTeller.Application.Interfaces
{
    public interface IContaRepository
    {
        Task<Conta?> ObterPorDocumentoAsync(string documento);
        Task<IEnumerable<Conta?>> ObterPorNomeAsync(string nome);
        Task CriarAsync(Conta conta);
        Task AtualizarAsync(Conta conta);
        Task RegistrarInativaLogsAsync(InativaLog log);
    }
}
