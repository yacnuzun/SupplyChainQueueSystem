using Newtonsoft.Json;
using Shared.Constant;
using Shared.Dto_s;
using Shared.Events;
using Shared.Helpers.ResponseModels.GenericResultModels;
using SupplierAPI.Domain.Entities;
using SupplierAPI.Dto_s;
using SupplierAPI.Infrastructure.Repositories.Interfaces;
using System.Net.Http.Headers;

namespace SupplierAPI.Infrastructure.Repositories.Implemantations
{
    public class SupplierHelper : ISupplierHelper
    {
        private static HttpClient client = new HttpClient();
        private string role = nameof(Supplier);

        public async Task<IDataResult<JwtDto>> CheckUser(string token)
        {
            var headerAutho = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));

            client.DefaultRequestHeaders.Authorization = headerAutho;

            var request = await client.PostAsJsonAsync("https://localhost:44340/Account/loginAcces", new UserForLoginAccessDto { Role = role });

            if (!request.IsSuccessStatusCode)
            {
                return new ErrorDataResult<JwtDto>(Messages.FailedProccess);
            }

            var response = await request.Content.ReadAsStringAsync();

            JwtDto success = JsonConvert.DeserializeObject<JwtDto>(response);

            if (success is null)
            {
                return new ErrorDataResult<JwtDto>(Messages.FailedProccess);
            }

            return new SuccessDataResult<JwtDto>(success, Messages.SuccessProccess);
        }

        public async Task<IDataResult<EarlyPaymentEvent>> CreateAEarlyTask(string invoiceNumber, string token)
        {
            var headerAutho = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));

            client.DefaultRequestHeaders.Authorization = headerAutho;

            var request = await client.PostAsJsonAsync("https://localhost:7221/bill/paymentarequest", new PaymentRequestControllerDto { InvoiceNumber = invoiceNumber });

            if (!request.IsSuccessStatusCode)
            {
                return new ErrorDataResult<EarlyPaymentEvent>(Messages.FailedProccess);
            }

            string response = await request.Content.ReadAsStringAsync();

            PaymentRequestDto dto = JsonConvert.DeserializeObject<PaymentRequestDto>(response);

            if (dto is null)
            {
                return new ErrorDataResult<EarlyPaymentEvent>(Messages.FailedProccess);
            }


            var financialRequest = await client.GetFromJsonAsync<bool>("https://localhost:7007/Financial/earlypaymentrequest?invoiceNumber=" + invoiceNumber);

            if (!financialRequest)
            {
                return new ErrorDataResult<EarlyPaymentEvent>(Messages.FailedProccess);
            }

            return new SuccessDataResult<EarlyPaymentEvent>(EarlyPaymentEvent.GetViewModel(dto), Messages.SuccessProccess);



        }

        public async Task<IDataResult<List<BillListingDTO>>> GetBillswithSupplier(string supplierTaxId, string token)
        {
            var headerAutho = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));

            client.DefaultRequestHeaders.Authorization = headerAutho;

            var request = await client.GetFromJsonAsync<List<BillListingDTO>>("https://localhost:7221/bill/getbillsupplier?supplierTaxId=" + supplierTaxId);

            if (request is null)
            {
                return new ErrorDataResult<List<BillListingDTO>>(Messages.FailedProccess);
            }

            return new SuccessDataResult<List<BillListingDTO>>(request);

        }
    }
}
