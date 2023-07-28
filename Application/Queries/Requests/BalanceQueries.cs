using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Queries.Interfaces;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Queries.Requests
{
    public class BalanceQueries : IBalanceQueries
    {
        private readonly DatabaseConfig databaseConfig;
        public BalanceQueries(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public IEnumerable<BalanceResponse> GetBalanceWithMovementQuery(Guid currentAccountId)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            return connection.Query<BalanceResponse>($"SELECT " +
                                                        $"acc.nome as [NAME], " +
                                                        $"mov.idcontacorrente as [CURRENTLYACCOUNTID], " +
                                                        $"(SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = '{currentAccountId.ToString().ToUpper()}' AND tipomovimento = 'C') -" +
                                                        $"(SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = '{currentAccountId.ToString().ToUpper()}' AND tipomovimento = 'D') AS [CURRENTLYBALANCE]" +
                                                      $"FROM " +
                                                        $"movimento mov " +
                                                        $"INNER JOIN contacorrente acc ON (acc.idcontacorrente == mov.idcontacorrente) " +
                                                      $"WHERE " +
                                                        $"mov.idcontacorrente = '{currentAccountId.ToString().ToUpper()}' " +
                                                      $"GROUP BY " +
                                                        $"mov.idcontacorrente");
        }

        public IEnumerable<BalanceResponse> GetBalanceWithoutMovementQuery(Guid currentAccountId)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            return connection.Query<BalanceResponse>($"SELECT " +
                                                        $"nome as [NAME], " +
                                                        $"idcontacorrente as [CURRENTLYACCOUNTID] " +
                                                     $"FROM " +
                                                        $"contacorrente " +
                                                     $"WHERE " +
                                                        $"idcontacorrente = '{currentAccountId.ToString().ToUpper()}'");
        }

        public decimal GetCurrentlyBalanceQuery(Guid currentAccountId)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            return connection.Query<decimal>($"SELECT " +
                                                $"(SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = '{currentAccountId.ToString().ToUpper()}' AND tipomovimento = 'C') -" +
                                                $"(SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = '{currentAccountId.ToString().ToUpper()}' AND tipomovimento = 'D') AS [CURRENTLYBALANCE]" +
                                             $"FROM " +
                                                $"movimento mov " +
                                                $"INNER JOIN contacorrente acc ON (acc.idcontacorrente == mov.idcontacorrente) " +
                                             $"WHERE " +
                                                $"mov.idcontacorrente = '{currentAccountId.ToString().ToUpper()}' " +
                                             $"GROUP BY " +
                                                $"mov.idcontacorrente").FirstOrDefault();
        }
    }
}
