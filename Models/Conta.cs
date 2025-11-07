using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UVVFintech_Grupo3.Models;

namespace UVVFintech_Grupo3.Models
{
    public abstract class Conta
    {
        public int Id { get; set; }
        public string Numero { get; set; } = string.Empty;
        public decimal Saldo { get; set; }


        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}

