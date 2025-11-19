namespace UVVFintech_Grupo3.Models
{
    public abstract class Conta : IContaOperacoes //OCP - Permite a extensão através de subclasses sem modificar o código existente
    {
        public int Id { get; set; }
        public string Numero { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public int ClienteId { get; set; }
        public string? TipoConta { get; set; }

        public Cliente? Cliente { get; set; }
        public virtual void Depositar(decimal valor)
        {
            if (valor <= 0)
                throw new Exception("O valor deve ser maior que zero.");

            Saldo += valor;
        }

        public virtual void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new Exception("O valor deve ser maior que zero.");

            if (Saldo < valor)
                throw new Exception("Saldo insuficiente.");

            Saldo -= valor;
        }
    }
}