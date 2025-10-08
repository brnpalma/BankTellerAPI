using BankTellerAPI.Domain.Entities;

namespace BankTeller.Application.Interfaces
{
    public interface IBankService
    {
        void CriarConta(string nomeCliente, string documentoCliente);
        Task<Conta> ConsultarContasPorDocumento(string documentoCliente);
        Task<IEnumerable<Conta>> ConsultarContasPorNomeCliente(string nomeCliente);
        void Transferir(string docOrigem, string docDestino, decimal valor);
    }
}
