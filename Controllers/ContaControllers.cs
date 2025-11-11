using Microsoft.EntityFrameworkCore;
using UVVFintech_Grupo3.Models;
using UVVFintech_Grupo3.Persistencia;

namespace UVVFintech_Grupo3.Controllers
{
    public class ContaController
    {
        private readonly AppDbContext _db = new();

        public void Depositar(int contaId, decimal valor)
        {
            var conta = _db.Contas.Find(contaId);
            if (conta == null) return;

            conta.Saldo += valor;

            _db.Transacoes.Add(new Transacao
            {
                Tipo = "Depósito",
                Valor = valor,
                Data = DateTime.Now,
                ContaOrigemId = contaId
            });

            _db.SaveChanges();
        }

        public void Sacar(int contaId, decimal valor)
        {
            var conta = _db.Contas.Find(contaId);
            if (conta == null || conta.Saldo < valor) return;

            conta.Saldo -= valor;

            _db.Transacoes.Add(new Transacao
            {
                Tipo = "Saque",
                Valor = valor,
                Data = DateTime.Now,
                ContaOrigemId = contaId
            });

            _db.SaveChanges();
        }

        public void Transferir(int contaOrigemId, int contaDestinoId, decimal valor)
        {
            var contaOrigem = _db.Contas.Find(contaOrigemId);
            var contaDestino = _db.Contas.Find(contaDestinoId);

            if (contaOrigem == null || contaDestino == null)
                throw new Exception("Conta não encontrada.");

            if (contaOrigem.Saldo < valor)
                throw new Exception("Saldo insuficiente.");

            // Atualiza saldos
            contaOrigem.Saldo -= valor;
            contaDestino.Saldo += valor;

            // Registra transação
            _db.Transacoes.Add(new Transacao
            {
                Tipo = "Transferência",
                Valor = valor,
                Data = DateTime.Now,
                ContaOrigemId = contaOrigemId,
                ContaDestinoId = contaDestinoId
            });

            _db.SaveChanges();
        }


        //--------------------------------------------------------------//

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

        public void CriarConta(string cpfCliente, string tipo)
        {
            if (string.IsNullOrWhiteSpace(cpfCliente) || cpfCliente.Length != 11 || !cpfCliente.All(char.IsDigit))
                throw new ArgumentException("Informe o CPF do cliente que sejá válido.");

            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("Selecione o tipo da conta.");

            var cliente = _db.Clientes.FirstOrDefault(c => c.Cpf == cpfCliente)
                ?? throw new Exception("Nenhum cliente encontrado com esse CPF.");

            bool jaPossuiContaMesmoTipo = _db.Contas.Any(c => c.ClienteId == cliente.Id && EF.Property<string>(c, "TipoConta") == tipo);

            if (jaPossuiContaMesmoTipo)
                throw new Exception("O cliente já possui uma conta desse tipo.");

            Conta novaConta;

            if (tipo == "Corrente")
                novaConta = new ContaCorrente
                {
                    ClienteId = cliente.Id,
                    Saldo = 0,
                    Numero = Guid.NewGuid().ToString().Substring(0, 8),
                    Limite = 1000
                };
            else if (tipo == "Poupança" || tipo == "Poupanca")
            {
                novaConta = new Poupanca
                {
                    ClienteId = cliente.Id,
                    Saldo = 0,
                    Numero = Guid.NewGuid().ToString().Substring(0, 8),
                    TaxaRendimento = 0.02
                };
            }
            else
                throw new Exception("Tipo de conta inválido.");

            _db.Contas.Add(novaConta);
            _db.SaveChanges();
        }

        public void ExcluirConta(int id)
        {
            var conta = _db.Contas.Find(id)
                ?? throw new Exception("Conta não encontrada.");

            // Impedir exclusão se tiver transações 
            bool temMovimentacoes = _db.Transacoes
                .Any(t => t.ContaOrigemId == id || t.ContaDestinoId == id);

            if (temMovimentacoes)
                throw new Exception("Não é possível excluir uma conta com movimentações.");

            _db.Contas.Remove(conta);
            _db.SaveChanges();
        }
    }
}
