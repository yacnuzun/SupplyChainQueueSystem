using Shared.Dto_s;
using Shared.Events;
using Shared.Helpers.ResponseModels.GenericResultModels;
using SupplierAPI.Dto_s;

namespace SupplierAPI.Infrastructure.Repositories.Interfaces
{
    public interface ISupplierHelper
    {
        public Task<IDataResult<EarlyPaymentEvent>> CreateAEarlyTask(string invoiceNumber, string token);
        public Task<IDataResult<JwtDto>> CheckUser(string token);
        public Task<IDataResult<List<BillListingDTO>>> GetBillswithSupplier(string supplierTaxId, string token);
    }
}
