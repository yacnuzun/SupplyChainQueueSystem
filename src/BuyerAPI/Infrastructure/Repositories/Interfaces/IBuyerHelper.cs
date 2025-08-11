using BuyerAPI.Dto_s;
using Shared.Dto_s;
using Shared.Events;
using Shared.Helpers.ResponseModels.GenericResultModels;

namespace BuyerAPI.Infrastructure.Repositories.Interfaces
{
    public interface IBuyerHelper
    {
        public Task<BillEvent> CreateABill(CreateBillDTO dto, string token);
        public Task<IDataResult<JwtDto>> CheckUser(string token);
        public Task<IDataResult<List<BillListingDTO>>> GetBills(string buyerTaxId, string token);
    }
}
