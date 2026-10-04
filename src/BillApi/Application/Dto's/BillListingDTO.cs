using BillApi.Domain.Entities;
using Shared.Abstract;

namespace BillApi.Dto_s
{
    public class BillListingDTO: IDTO
    {
        public string InvoiceNumber { get; set; }
        public string Buyer { get; set; }
        public string Supplier { get; set; }
        public Status Status { get; set; }
        public decimal InvoiceCost { get; set; }

        public static BillListingDTO GetViewModel(Bill model)
        {
            return new BillListingDTO
            {
                InvoiceNumber = model.InvoiceNumber,
                Buyer = model.BuyerTaxID,
                Supplier = model.SuplierTaxID,
                InvoiceCost = model.InvoiceCost,
                Status = model.InovoiceStatus
            };
        }
    }

}
