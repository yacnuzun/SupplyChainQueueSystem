using Shared.Abstract;

namespace BillApi.Dto_s
{

    public class PaymentRequestControllerDto:IDTO
    {
        public string InvoiceNumber { get; set; }
    }

}
