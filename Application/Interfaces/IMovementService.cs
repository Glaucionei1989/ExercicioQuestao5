using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Application.Interfaces
{
    public interface IMovementService
    {
        string ValidateMovement(Movement movement);
    }
}