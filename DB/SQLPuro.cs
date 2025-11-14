using Microsoft.Data.SqlClient;
using UVVFintech_Grupo3.Models;

namespace UVVFintech_Grupo3.DB
{
    public class SQLPuro
    {
        private readonly string _connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=UVVFintechGrupo3DB;Trusted_Connection=True;";

        public List<Transacao> BuscarExtratoPorPeriodo(DateTime inicio, DateTime fim, int contaId)
        {
            var lista = new List<Transacao>();

            using var conexao = new SqlConnection(_connectionString);
            conexao.Open();

            string sql = @"
                SELECT Id, Tipo, Valor, Data, ContaOrigemId, ContaDestinoId
                FROM Transacoes
                WHERE Data BETWEEN @Inicio AND @Fim
                  AND (@Conta = ContaOrigemId OR @Conta = ContaDestinoId)
                ORDER BY Data DESC;
            ";

            using var cmd = new SqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@Inicio", inicio);
            cmd.Parameters.AddWithValue("@Fim", fim);
            cmd.Parameters.AddWithValue("@Conta", contaId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Transacao
                {
                    Id = reader.GetInt32(0),
                    Tipo = reader.GetString(1),
                    Valor = reader.GetDecimal(2),
                    Data = reader.GetDateTime(3),
                    ContaOrigemId = reader.GetInt32(4),
                    ContaDestinoId = reader.IsDBNull(5) ? null : reader.GetInt32(5)
                });
            }

            return lista;
        }
    }
}
