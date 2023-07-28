using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Interfaces;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]/Save")]
    public class MovementController : ControllerBase
    {
        private readonly IMovementService movementService;
        private readonly IIdEmpotenciaService idEmpotenciaService;
        private int numberAttempts = 1;

        public MovementController(IMovementService movementService, IIdEmpotenciaService idEmpotenciaService)
        {
            this.movementService = movementService;
            this.idEmpotenciaService = idEmpotenciaService;
        }

        [HttpPost]
        public IActionResult Save([FromBody] Movement currentlyMovement)
        {
            try
            {
                var resultMovimentId = movementService.ValidateMovement(currentlyMovement);
                return Ok(new MovementResponse { IdMovimento = new Guid(resultMovimentId) });
            }
            catch (Exception e)
            {
                ReprocessRequest(currentlyMovement, e.Message);
                return BadRequest(e.Message);
            }
        }

        private void ReprocessRequest(Movement currentlyMovement, string message)
        {
            idEmpotenciaService.Update(currentlyMovement.Chave_IdEmpotencia.ToString().ToUpper(), $"ERROR, Tentativa {numberAttempts} de 5! - {message}");

            var requestOk = idEmpotenciaService.GetIdEmpotenciaByKeyQuery(currentlyMovement.IdContaCorrente.ToString().ToUpper());
            if (!requestOk.Any() && numberAttempts < 5)
            {
                numberAttempts++;
                if (currentlyMovement != null)
                    Save(currentlyMovement);

            }   
        }
    }
}