namespace UVVFintech_Grupo3.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;

        public List<Conta>? Contas { get; set; }
    }
}

