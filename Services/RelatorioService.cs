using Microsoft.Data.SqlClient;
using System.Data;
using UVVFintech_Grupo3.Models;

namespace UVVFintech_Grupo3.Services
{
    public class RelatorioService
    {
        private readonly string _connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=UVVFintechGrupo3DB;Trusted_Connection=True;";

        public List<Transacao> BuscarExtratoPorPeriodo(DateTime inicio, DateTime fim)
        {
            var lista = new List<Transacao>();

            using var conexao = new SqlConnection(_connectionString);
            conexao.Open();

            string sql = @"
                SELECT Id, Tipo, Valor, Data, ContaOrigemId, ContaDestinoId
                FROM Transacoes
                WHERE Data BETWEEN @Inicio AND @Fim";

            using var cmd = new SqlCommand(sql, conexao);
            cmd.Parameters.Add("@Inicio", SqlDbType.DateTime).Value = inicio;
            cmd.Parameters.Add("@Fim", SqlDbType.DateTime).Value = fim;

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
