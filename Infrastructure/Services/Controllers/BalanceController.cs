using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Interfaces;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]/CheckBalance")]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceService balanceService;

        public BalanceController(IBalanceService balanceService)
        {
            this.balanceService = balanceService;
        }

        [HttpGet]
        public IActionResult GetBalance(Guid currentAccountId)
        {
            try
            {
                var resultBalance = balanceService.GetBalance(currentAccountId);
                return Ok(resultBalance);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}