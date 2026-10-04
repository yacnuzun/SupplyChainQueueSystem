using FinancialAPI.Infrastructure.Repositories.Interfaces;
using Shared.Constant;
using Shared.Helpers.ResponseModels.GenericResultModels;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace FinancialAPI.Infrastructure.Repositories.Implemantations
{
    public class FinancialHelper : IFinancialHelper
    {
        private static HttpClient client = new HttpClient();
        public async Task<IResult> EarlypPaymentRequest(string invoiceNumber)
        {
            var request = await client.GetFromJsonAsync<bool>("http://localhost:5001/bill/paymentresponse?invoiceNumber=" + invoiceNumber);

            if (!request)
            {
                return new ErrorResult(Messages.FailedProccess);
            }

            return new SuccesResult();
        }
    }
}
