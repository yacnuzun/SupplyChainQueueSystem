using Shared.Abstract;

namespace SupplierAPI.Dto_s
{
    public class PaymentRequestControllerDto : IDTO
    {
        public string InvoiceNumber { get; set; }
    }
}
