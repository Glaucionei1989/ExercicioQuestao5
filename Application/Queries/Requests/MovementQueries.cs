using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Extensions;
using Questao5.Application.Queries.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Queries.Requests
{
    public class MovementQueries : IMovementQueries
    {
        private readonly DatabaseConfig databaseConfig;
        public MovementQueries(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public string InsertMovementQuery(Movement movement)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var movementId = Guid.NewGuid().ToString().ToUpper();

            connection.Execute($"INSERT INTO movimento(idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) " +
                $"VALUES('{movementId}', '{movement.IdContaCorrente.ToString().ToUpper()}', '{DateTime.Now}', '{movement.TipoMovimento.GetDescription()}', '{movement.Valor}');");

            return movementId;

        }
    }
}
