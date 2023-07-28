using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Interfaces
{
    public interface IBalanceQueries
    {
        IEnumerable<BalanceResponse> GetBalanceWithMovementQuery(Guid currentAccountId);
        IEnumerable<BalanceResponse> GetBalanceWithoutMovementQuery(Guid currentAccountId);
        decimal GetCurrentlyBalanceQuery(Guid currentAccountId);
    }
}