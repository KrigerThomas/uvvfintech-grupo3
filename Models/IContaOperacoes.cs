namespace UVVFintech_Grupo3.Models
{
    public interface IContaOperacoes
    {
        void Depositar(decimal valor);
        void Sacar(decimal valor);
    }
}
