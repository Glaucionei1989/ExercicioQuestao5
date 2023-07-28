using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Queries.Interfaces;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Questao5.Application.Queries.Requests
{
    public class CurrentAccountQueries : ICurrentAccountQueries
    {
        private readonly DatabaseConfig databaseConfig;
        public CurrentAccountQueries(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public IEnumerable<CurrentAccount> GetContaCorrenteByIdQuery(Guid currentAccountId)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            return connection.Query<CurrentAccount>($"SELECT * FROM contacorrente WHERE idcontacorrente = '{currentAccountId.ToString().ToUpper()}';");
        }
    }
}
