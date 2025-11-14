using Microsoft.EntityFrameworkCore;
using UVVFintech_Grupo3.DB;
using UVVFintech_Grupo3.Models;

namespace UVVFintech_Grupo3.Controllers
{
    public class ClienteController
    {
        private readonly AppDbContext _db = new();

        public List<Cliente> ListarTodos()
        {
            return _db.Clientes.Include(c => c.Contas).ToList();
        }
        public Cliente BuscarPorCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("Informe um CPF para buscar.");

            return _db.Clientes.FirstOrDefault(c => c.Cpf == cpf)
                ?? throw new Exception("Nenhum cliente encontrado com esse CPF.");
        }

        public void Cadastrar(string nome, string cpf)
        {
            if (string.IsNullOrWhiteSpace(nome) || nome.Any(char.IsDigit)) 
                throw new ArgumentException("Insira um nome válido");

            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || !cpf.All(char.IsDigit))
                throw new ArgumentException("Insira um CPF com 11 dígitos válido.");

            var existe = _db.Clientes.FirstOrDefault(c => c.Cpf == cpf);
            if (existe != null)
                throw new Exception("Já existe um cliente com esse CPF.");

            var cliente = new Cliente { Nome = nome, Cpf = cpf };
            _db.Clientes.Add(cliente);
            _db.SaveChanges();
        }

        public void Excluir(int id)
        {
            var cliente = _db.Clientes.Include(c => c.Contas).FirstOrDefault(c => c.Id == id);

            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            if (cliente.Contas != null && cliente.Contas.Any())
                throw new Exception("Não é possível excluir um cliente com contas vinculadas.");

            _db.Clientes.Remove(cliente);
            _db.SaveChanges();
        }
    }
}

