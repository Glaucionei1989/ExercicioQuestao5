using Microsoft.Data.Sqlite;
using Questao5.Application.Extensions;
using Questao5.Application.Interfaces;
using Questao5.Application.Queries.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using System.Globalization;

namespace Questao5.Infrastructure.Services
{
    public class MovementService : IMovementService
    {
        private readonly IBalanceQueries balanceQueries;
        private readonly ICurrentAccountQueries currentAccountQueries;
        private readonly IMovementQueries movementQueries;
        private readonly IIdEmpotenciaService idEmpotenciaService
            ;
        public MovementService(IBalanceQueries balanceQueries, ICurrentAccountQueries currentAccountQueries, IMovementQueries movementQueries, IIdEmpotenciaService idEmpotenciaService)
        {
            this.balanceQueries = balanceQueries;
            this.currentAccountQueries = currentAccountQueries;
            this.movementQueries = movementQueries;
            this.idEmpotenciaService = idEmpotenciaService;
        }
        public string ValidateMovement(Movement movement)
        {
            ValidateKeyRequest(movement.Chave_IdEmpotencia.ToString().ToUpper());
            ValidateAccount(movement.IdContaCorrente);
            ValidateMovementValue(decimal.Parse(movement.Valor, NumberStyles.Number));

            if (movement.TipoMovimento == MovementTypeEnum.Debit)
                ValidateDebitValue(decimal.Parse(movement.Valor, NumberStyles.Number), movement.IdContaCorrente);

            ValidateMovementType(movement.TipoMovimento);

            var resultSuccessId = movementQueries.InsertMovementQuery(movement);

            if (resultSuccessId != null)
                idEmpotenciaService.Delete(movement.Chave_IdEmpotencia.ToString().ToUpper());

            return resultSuccessId;

        }

        private void ValidateKeyRequest(string key)
        {
            var resutlIdEmpotencia = idEmpotenciaService.GetIdEmpotenciaByKeyQuery(key);

            if (!resutlIdEmpotencia.Any())
            {
                idEmpotenciaService.Save(key, "[POST] /movement", "");
                return;
            }
        }

        private void ValidateAccount(Guid currentAccountId)
        {
            var account = currentAccountQueries.GetContaCorrenteByIdQuery(currentAccountId);

            if (!account.Any())
                throw new Exception(ExceptionMessages.INVALID_ACCOUNT.GetDescription());

            if (account.Any() && !account.FirstOrDefault().Ativo)
                throw new Exception(ExceptionMessages.INACTIVE_ACCOUNT.GetDescription());
        }

        private void ValidateMovementValue(decimal movementValue)
        {
            if (movementValue < 0)
                throw new Exception(string.Format(ExceptionMessages.INVALID_VALUE.GetDescription(), movementValue.ToString()));
        }

        private void ValidateMovementType(MovementTypeEnum? movementType)
        {
            if (movementType != MovementTypeEnum.Debit && movementType != MovementTypeEnum.Credit)
                throw new Exception(string.Format(ExceptionMessages.INVALID_TYPE.GetDescription(), movementType.ToString()));
        }

        private void ValidateDebitValue(decimal movementValue, Guid currentAccountId)
        {
            var valueCredit = balanceQueries.GetCurrentlyBalanceQuery(currentAccountId);

            if (valueCredit <= movementValue)
                throw new Exception(string.Format(ExceptionMessages.INVALID_MOVEMENT.GetDescription(), valueCredit.ToString()));
        }
    }
}
