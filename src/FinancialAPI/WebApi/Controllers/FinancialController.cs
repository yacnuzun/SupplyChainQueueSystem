using FinancialAPI.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace FinancialAPI.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinancialController : ControllerBase
    {
        readonly IFinancialHelper _financialHelper;
        public FinancialController(IFinancialHelper financialHelper)
        {
            _financialHelper = financialHelper;
        }

        [HttpGet("earlypaymentrequest")]
        public async Task<bool> EarlypPaymentRequest(string invoiceNumber)
        {
            var response = await _financialHelper.EarlypPaymentRequest(invoiceNumber);

            if (!response.Success)
            {
                return false;
            }

            return true;
        }
    }
}
