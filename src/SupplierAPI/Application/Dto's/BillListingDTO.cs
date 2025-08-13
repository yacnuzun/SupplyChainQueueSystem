using SupplierAPI.Domain.Entities;

namespace SupplierAPI.Dto_s
{
    public class BillListingDTO
    {
        public string InvoiceNumber { get; set; }
        public string Buyer { get; set; }
        public string Supplier { get; set; }
        public Status Status { get; set; }
        public decimal InvoiceCost { get; set; }

    }
}
