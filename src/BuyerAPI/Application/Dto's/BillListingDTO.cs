using BuyerAPI.Domain.Entities;
using Shared.Abstract;

namespace BuyerAPI.Dto_s
{
    public class BillListingDTO:IDTO
    {
        public string InvoiceNumber { get; set; }
        public string Buyer { get; set; }
        public string Supplier { get; set; }
        public Status Status { get; set; }
        public decimal InvoiceCost { get; set; }

    }
}
