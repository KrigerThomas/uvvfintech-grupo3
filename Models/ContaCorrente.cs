namespace UVVFintech_Grupo3.Models
{
    public class ContaCorrente : Conta
    {
        public decimal Limite { get; set; }

        public ContaCorrente()
        {
            TipoConta = "Corrente";
        }

        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new Exception("O valor deve ser maior que zero.");

            if (Saldo - valor < -Limite)
                throw new Exception("Saldo insuficiente, considerando o limite.");

            Saldo -= valor;
        }

    }
}
