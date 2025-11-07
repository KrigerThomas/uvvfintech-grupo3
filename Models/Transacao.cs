using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UVVFintech_Grupo3.Models;

namespace UVVFintech_Grupo3.Models
{
    public class Transacao
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty; // "Depósito", "Saque", "Transferência"
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }

        public int ContaOrigemId { get; set; }
        public int? ContaDestinoId { get; set; }

        public Conta? ContaOrigem { get; set; }
        public Conta? ContaDestino { get; set; }
    }
}
