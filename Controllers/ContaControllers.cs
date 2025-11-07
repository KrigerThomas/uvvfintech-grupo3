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

        public void CriarConta(string cpfCliente, string tipo)
        {
            var cliente = _db.Clientes.FirstOrDefault(c => c.Cpf == cpfCliente);
            if (cliente == null) throw new Exception("Cliente não encontrado.");

            Conta novaConta;

            if (tipo == "Corrente")
                novaConta = new ContaCorrente { ClienteId = cliente.Id, Saldo = 0, Limite = 1000 };
            else
                novaConta = new Poupanca { ClienteId = cliente.Id, Saldo = 0, TaxaRendimento = 0.02 };

            _db.Contas.Add(novaConta);
            _db.SaveChanges();
        }
        public object ListarContas()
        {
            return _db.Contas.ToList();
        }

        public void ExcluirConta(int id)
        {
            var conta = _db.Contas.Find(id);
            if (conta != null)
            {
                _db.Contas.Remove(conta);
                _db.SaveChanges();
            }
        }
    }
}
