using Microsoft.EntityFrameworkCore;
using UVVFintech_Grupo3.DB;
using UVVFintech_Grupo3.Models;

namespace UVVFintech_Grupo3.Controllers
{
    public class ContaController
    {
        private readonly AppDbContext _db = new();
        private readonly SQLPuro _sql = new();

        public void CriarConta(string cpfCliente, string tipo)
        {
            ValidarEntradaCriar(cpfCliente, tipo);

            var cliente = BuscarCliente(cpfCliente);

            VerificarSeJaExisteConta(cliente.Id, tipo);

            var conta = CriarInstanciaDeConta(cliente.Id, tipo);

            _db.Contas.Add(conta);
            _db.SaveChanges();
        }

        private void ValidarEntradaCriar(string cpfCliente, string tipo)
        {
            if (string.IsNullOrWhiteSpace(cpfCliente) ||
                cpfCliente.Length != 11 ||
                !cpfCliente.All(char.IsDigit))
                throw new ArgumentException("Informe um CPF válido com 11 dígitos.");

            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("Selecione o tipo da conta.");
        }

        private Cliente BuscarCliente(string cpfCliente)
        {
            return _db.Clientes.FirstOrDefault(c => c.Cpf == cpfCliente)
                ?? throw new Exception("Nenhum cliente encontrado com esse CPF.");
        }

        private void VerificarSeJaExisteConta(int clienteId, string tipo)
        {
            bool jaExiste = _db.Contas.Any(c => c.ClienteId == clienteId && c.TipoConta == tipo);

            if (jaExiste)
                throw new Exception("O cliente já possui uma conta desse tipo.");
        }

        private Conta CriarInstanciaDeConta(int clienteId, string tipo)
        {
            string numero = Guid.NewGuid().ToString().Substring(0, 8);

            if (tipo == "Corrente")
            {
                return new ContaCorrente
                {
                    ClienteId = clienteId,
                    Numero = numero,
                    Saldo = 0,
                    Limite = 1000
                };
            }
            else if (tipo == "Poupança" || tipo == "Poupanca")
            {
                return new Poupanca
                {
                    ClienteId = clienteId,
                    Numero = numero,
                    Saldo = 0,
                    TaxaRendimento = 0.02
                };
            }
            else
            {
                throw new Exception("Tipo de conta inválido.");
            }
        }
        public List<Conta> ListarTodas()
        {
            return _db.Contas
                .Include(c => c.Cliente)
                .ToList();
        }

        public List<Conta> BuscarPorCpf(string cpf)
        {
            return _db.Contas
                .Include(c => c.Cliente)
                .Where(c => c.Cliente.Cpf == cpf)
                .ToList();
        }

        // OPERAÇÕES

        public void Depositar(int contaId, decimal valor)
        {
            var conta = _db.Contas.Find(contaId)
                ?? throw new Exception("Conta não encontrada.");

            conta.Depositar(valor);

            RegistrarTransacao("Depósito", valor, contaId, null);

            _db.SaveChanges();
        }

        public void Sacar(int contaId, decimal valor)
        {
            var conta = _db.Contas.Find(contaId)
                ?? throw new Exception("Conta não encontrada.");

            conta.Sacar(valor);

            RegistrarTransacao("Saque", valor, contaId, null);

            _db.SaveChanges();
        }

        public void Transferir(int origem, int destino, decimal valor)
        {
            if (origem == destino)
                throw new Exception("A conta de origem e destino devem ser diferentes.");

            var contaOrigem = _db.Contas.Find(origem)
                ?? throw new Exception("Conta de origem não encontrada.");

            var contaDestino = _db.Contas.Find(destino)
                ?? throw new Exception("Conta de destino não encontrada.");

            contaOrigem.Sacar(valor);
            contaDestino.Depositar(valor);

            RegistrarTransacao("Transferência", valor, origem, destino);

            _db.SaveChanges();
        }

        private void RegistrarTransacao(string tipo, decimal valor, int origem, int? destino)
        {
            _db.Transacoes.Add(new Transacao
            {
                Tipo = tipo,
                Valor = valor,
                Data = DateTime.Now,
                ContaOrigemId = origem,
                ContaDestinoId = destino
            });
        }

        public void ExcluirConta(int id)
        {
            var conta = _db.Contas.Find(id)
                ?? throw new Exception("Conta não encontrada.");

            bool possuiMov = _db.Transacoes
                .Any(t => t.ContaOrigemId == id || t.ContaDestinoId == id);

            if (possuiMov)
                throw new Exception("Não é possível excluir uma conta com movimentações.");

            _db.Contas.Remove(conta);
            _db.SaveChanges();
        }

        public List<Transacao> ExtratoSql(DateTime inicio, DateTime fim, int contaId)
        {
            return _sql.BuscarExtratoPorPeriodo(inicio, fim, contaId);
        }
    }
}
