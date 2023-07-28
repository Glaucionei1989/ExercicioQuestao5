using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Queries.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Queries.Requests
{
    public class IdEmpotenciaQueries : IIdEmpotenciaQueries
    {
        private readonly DatabaseConfig databaseConfig;
        public IdEmpotenciaQueries(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public string InsertIdEmpotenciaQuery(string key, string request, string result)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            connection.Execute($"INSERT INTO idempotencia(chave_idempotencia, requisicao, resultado) " +
                $"VALUES('{key}', '{request}', '{result}');");

            return key;

        }

        public void UpdateIdEmpotenciaQuery(string key, string result)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            connection.Execute($"UPDATE idempotencia SET resultado = '{result}' WHERE chave_idempotencia = '{key}'");

        }

        public void DeleteIdEmpotenciaQuery(string key)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            connection.Execute($"DELETE FROM idempotencia WHERE chave_idempotencia = '{key}'");

        }

        public IEnumerable<IdEmpotencia> GetIdEmpotenciaByKeyQuery(string key)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            return connection.Query<IdEmpotencia>($"SELECT * FROM idempotencia WHERE chave_idempotencia = '{key}';");
        }
    }
}
