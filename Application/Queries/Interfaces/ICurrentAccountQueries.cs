using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Interfaces
{
    public interface ICurrentAccountQueries
    {
        IEnumerable<CurrentAccount> GetContaCorrenteByIdQuery(Guid currentAccountId);
    }
}