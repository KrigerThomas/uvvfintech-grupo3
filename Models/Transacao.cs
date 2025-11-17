namespace UVVFintech_Grupo3.Models
{
    public class Transacao
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty; 
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }

        public int ContaOrigemId { get; set; }
        public int? ContaDestinoId { get; set; }

        public Conta? ContaOrigem { get; set; }
        public Conta? ContaDestino { get; set; }
    }
}
