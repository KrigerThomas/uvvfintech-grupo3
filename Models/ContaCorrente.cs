using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UVVFintech_Grupo3.Models;

namespace UVVFintech_Grupo3.Models
{
    public class ContaCorrente : Conta
    {
        public decimal Limite { get; set; }
    }
}

