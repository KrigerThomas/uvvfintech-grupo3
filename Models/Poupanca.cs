namespace UVVFintech_Grupo3.Models
{
    public class Poupanca : Conta
    {
        public double TaxaRendimento { get; set; }

        public Poupanca()
        {
            TipoConta = "Poupança"; 
        }

        public override void Depositar(decimal valor)
        {
            if (valor <= 0)
                throw new Exception("O valor deve ser maior que zero.");

            //Saldo += valor;
            Saldo += valor * (1+(decimal)TaxaRendimento);

            //0 = 0 + 1000 * (1 + 0.02) = 1020;
            //1020 = 1020 + 1000 * (1 + 0.02) = 2040;

        }

    }
}
