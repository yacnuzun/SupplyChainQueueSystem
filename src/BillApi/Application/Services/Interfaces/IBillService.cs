using BillApi.Dto_s;
using Shared.Helpers.ResponseModels.GenericResultModels;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace BillApi.Application.Services.Interfaces
{
    public interface IBillService
    {
        Task<IDataResult<BillResponseDto>> CreateABill(CreateBillDTO dto);
        Task<IDataResult<PaymentRequestDto>> CreatePaymentRequest(string invoiceNumber);
        Task<IResult> GetPaymentResponse(string invoiceNumber);
        Task<IDataResult<List<BillListingDTO>>> GetBillDtowithBuyerID(string buyerTaxId);
        Task<IDataResult<List<BillListingDTO>>> GetBillDtowithSupplierID(string supplierTaxId);
    }
}
