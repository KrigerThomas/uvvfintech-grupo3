using Microsoft.EntityFrameworkCore;
using System;
using UVVFintech_Grupo3.Persistencia;
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

        public void Cadastrar(string nome, string cpf)
        {
            var cliente = new Cliente { Nome = nome, Cpf = cpf };
            _db.Clientes.Add(cliente);
            _db.SaveChanges();
        }

        public void Excluir(int id)
        {
            var cliente = _db.Clientes.Find(id);
            if (cliente != null)
            {
                _db.Clientes.Remove(cliente);
                _db.SaveChanges();
            }
        }
    }
}

