namespace UVVFintech_Grupo3.Models
{
    public interface IContaOperacoes //ISP - Interface pequena e específica com apenas operações de conta
    {
        void Depositar(decimal valor);
        void Sacar(decimal valor);
    }
}
