using BankTeller.Application.Interfaces;
using BankTeller.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BankTeller.Application.Services
{
    public class BankService(IContaRepository repository, ILogger<BankService> iLogger) : IBankService
    {
        private readonly IContaRepository _repository = repository;
        private readonly ILogger<BankService> _logger = iLogger;

        public async Task<Resultado<Conta>> CadastrarConta(ContaRequest model)
        {
            var retorno = new Resultado<Conta>();

            var contaExistente = await _repository.ObterPorDocumentoAsync(model.Documento);

            if (contaExistente is not null)
            {
                retorno.Erro = "Já existe uma conta cadastrada com este documento.";
                return retorno;
            }

            var conta = new Conta(model.Nome, model.Documento);
            await _repository.CriarAsync(conta);

            retorno.Sucesso = true;
            retorno.Value = conta;
            return retorno;
        }

        public async Task<Resultado<Conta>> ConsultarContasPorDocumento(string documento)
        {
            var retorno = new Resultado<Conta>();

            var conta = await _repository.ObterPorDocumentoAsync(documento);

            if (conta is null)
            {
                retorno.Erro = "Nenhuma conta encontrada com o documento informado.";
                return retorno;
            }

            retorno.Sucesso = true;
            retorno.Value = conta;
            return retorno;
        }

        public async Task<Resultado<IEnumerable<Conta>>> ConsultarContasPorNome(string nome)
        {
            var retorno = new Resultado<IEnumerable<Conta>>();

            var listaContas = await _repository.ObterPorNomeAsync(nome);

            if (listaContas is null || !listaContas.Any())
            {
                retorno.Erro = "Nenhuma conta encontrada com o termo pesquisado.";
                return retorno;
            }

            retorno.Sucesso = true;
            retorno.Value = listaContas;
            return retorno;
        }

        public async Task<Resultado<string>> InativarConta(InativaRequest model)
        {
            var retorno = new Resultado<string>();

            var conta = _repository.ObterPorDocumentoAsync(model.Documento).Result;

            if (conta is null)
            {
                retorno.Erro = "Conta não encontrada para inativar.";
                return retorno;
            }

            if (!conta.Ativa)
            {
                retorno.Erro = "A conta já está inativa.";
                return retorno;
            }

            conta.Inativar();

            await _repository.AtualizarAsync(conta);

            var log = new InativaLog(model.Documento, model.NomeUsuario);
            await _repository.RegistrarInativaLogsAsync(log);
            _logger.LogWarning("Conta do documento {Documento} inativada por usuário: {Usuario}", model.Documento, model.NomeUsuario);

            retorno.Sucesso = true;
            retorno.Mensagem = "A Conta foi alterada para inativa com sucesso.";
            return retorno;
        }

        public async Task<Resultado<string>> Transferir(TransferenciaRequest model)
        {
            var retorno = new Resultado<string>();

            var contaOrigem = _repository.ObterPorDocumentoAsync(model.DocumentoOrigem).Result;
            var contaDestino = _repository.ObterPorDocumentoAsync(model.DocumentoDestino).Result;

            if (contaOrigem is null || contaDestino is null)
            {
                retorno.Erro = "Conta origem ou destino não encontrada.";
                return retorno;
            }

            if (!contaOrigem.Ativa || !contaDestino.Ativa)
            {
                retorno.Erro = "Conta origem ou destino inativa. Transferencias só podem ocorrer entre contas ativas.";
                return retorno;
            }

            if (contaOrigem.SaldoAtual < model.Valor)
            {
                retorno.Erro = "O saldo da conta é insuficiente para realizar a transferencia.";
                return retorno;
            }

            contaOrigem.Transferir(contaDestino, model.Valor);

            await _repository.AtualizarAsync(contaDestino);
            await _repository.AtualizarAsync(contaOrigem);

            retorno.Sucesso = true;
            retorno.Mensagem = "Transferência realizada com sucesso.";

            return retorno;
        }
    }
}
