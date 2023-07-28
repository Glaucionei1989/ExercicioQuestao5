using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Interfaces
{
    public interface IBalanceService
    {
        IEnumerable<BalanceResponse> GetBalance(Guid currentAccountId);
    }
}