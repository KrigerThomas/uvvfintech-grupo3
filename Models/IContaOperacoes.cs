using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVVFintech_Grupo3.Models
{
    public interface IContaOperacoes
    {
        void Depositar(decimal valor);
        void Sacar(decimal valor);
    }
}
