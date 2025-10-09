using BankTeller.Domain.Entities;

namespace BankTeller.Application.Interfaces
{
    public interface IBankService
    {
        Task<Resultado<Conta>> CadastrarConta(ContaDto model);
        Task<Resultado<Conta>> ConsultarContasPorDocumento(string documento);
        Task<Resultado<IEnumerable<Conta>>> ConsultarContasPorNome(string nome);
        Task<Resultado<string>> InativarConta(InativaDto model);
        Task<Resultado<string>> Transferir(TransacaoDto model);
    }
}
