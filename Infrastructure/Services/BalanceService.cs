using Questao5.Application.Extensions;
using Questao5.Application.Interfaces;
using Questao5.Application.Queries.Interfaces;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceQueries balanceQueries;
        private readonly ICurrentAccountQueries currentAccountQueries;

        public BalanceService(IBalanceQueries balanceQueries, ICurrentAccountQueries currentAccountQueries)
        {
            this.balanceQueries = balanceQueries;
            this.currentAccountQueries = currentAccountQueries;
        }

        public IEnumerable<BalanceResponse> GetBalance(Guid currentAccountId)
        {
            ValidateAccount(currentAccountId);

            var balance = balanceQueries.GetBalanceWithMovementQuery(currentAccountId);

            if (!balance.Any())
                balance = balanceQueries.GetBalanceWithoutMovementQuery(currentAccountId);

            balance.FirstOrDefault().CurrentlyDate = DateTime.Now;

            return balance;

        }

        private void ValidateAccount(Guid currentAccountId)
        {
            var account = currentAccountQueries.GetContaCorrenteByIdQuery(currentAccountId);

            if (!account.Any())
                throw new Exception(ExceptionMessages.INVALID_ACCOUNT.GetDescription());

            if (account.Any() && !account.FirstOrDefault().Ativo)
                throw new Exception(ExceptionMessages.INACTIVE_ACCOUNT.GetDescription());
        }
    }
}
