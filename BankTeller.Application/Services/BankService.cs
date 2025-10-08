using BankTeller.Application.Interfaces;
using BankTellerAPI.Application.Interfaces;
using BankTellerAPI.Domain.Entities;

namespace BankTeller.Application.Services
{
    internal class BankService(IContaRepository repository) : IBankService
    {
        private readonly IContaRepository _repository = repository;

        public void CriarConta(string nomeCliente, string documentoCliente)
        {
            var conta = new Conta(nomeCliente, documentoCliente);
            _repository.CriarAsync(conta);
        }

        public async Task<IEnumerable<Conta>> ConsultarContasPorNomeCliente(string nomeCliente)
        {
            var listaContas = await _repository.ObterPorNomeAsync(nomeCliente);
            return listaContas;
        }

        public async Task<Conta> ConsultarContasPorDocumento(string documentoCliente)
        {
            var conta = await _repository.ObterPorDocumentoAsync(documentoCliente);
            return conta;
        }

        public void Transferir(string docOrigem, string docDestino, decimal valor)
        {
            var contaOrigem = _repository.ObterPorDocumentoAsync(docOrigem).Result;
            var contaDestino = _repository.ObterPorDocumentoAsync(docDestino).Result;

            if (contaOrigem is null || contaDestino is null)
            {
                throw new InvalidOperationException("Conta origem ou destino não encontrada.");
            }

            contaOrigem.Transferir(contaDestino, valor);

            _repository.AtualizarAsync(contaDestino);
            _repository.AtualizarAsync(contaOrigem);
        }
    }
}
