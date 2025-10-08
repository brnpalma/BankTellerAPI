using BankTeller.Domain.Entities;

namespace BankTeller.Application.Interfaces
{
    public interface IBankService
    {
        Task<Resultado<Conta>> CadastrarConta(ContaRequest model);
        Task<Resultado<Conta>> ConsultarContasPorDocumento(string documento);
        Task<Resultado<IEnumerable<Conta>>> ConsultarContasPorNome(string nome);
        Task<Resultado<string>> InativarConta(InativaRequest model);
        Task<Resultado<string>> Transferir(TransferenciaRequest model);
    }
}
