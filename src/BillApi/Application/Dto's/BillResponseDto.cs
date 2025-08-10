using BillApi.Domain.Entities;
using Shared.Abstract;

namespace BillApi.Dto_s
{
    public class BillResponseDto: IDTO
    {
        public string TermDate { get; set; }
        public decimal InvoiceCost { get; set; }
        public string BuyerTaxID { get; set; }
        public string InvoiceNumber { get; set; }
        public string SuplierTaxID { get; set; }
        public Status InovoiceStatus { get; set; }

        public static BillResponseDto GetViewModel(Bill model)
        {
            return new BillResponseDto
            {
                TermDate = model.TermDate,
                InvoiceCost = model.InvoiceCost,
                BuyerTaxID = model.BuyerTaxID,
                InovoiceStatus = model.InovoiceStatus,
                InvoiceNumber = model.InvoiceNumber,
                SuplierTaxID = model.SuplierTaxID
            };
        }
    }

}
