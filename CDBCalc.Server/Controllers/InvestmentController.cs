using CDBCalc.Core.Service.Interface;
using CDBCalc.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace CDBCalc.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentCalculatorService _investmentCalculatorService;

        public InvestmentController(IInvestmentCalculatorService investmentCalculatorService)
        {
            _investmentCalculatorService = investmentCalculatorService ?? throw new ArgumentNullException(nameof(investmentCalculatorService));
        }

        [HttpPost("calculate-cdb")]
        public ActionResult<CalculationResponse> CalculateCdb([FromBody] CalculationRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null.");
            }

            var response = _investmentCalculatorService.CalculateCdb(request);
            return Ok(response);
        }
    }
}
