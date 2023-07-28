using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Interfaces
{
    public interface IMovementQueries
    {
        string InsertMovementQuery(Movement movement);
    }
}