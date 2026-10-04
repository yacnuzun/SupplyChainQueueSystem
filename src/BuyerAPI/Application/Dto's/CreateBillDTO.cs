using BuyerAPI.Domain.Entities;

namespace BuyerAPI.Dto_s
{
    public class CreateBillDTO
    {
        public string TermDate { get; set; }
        public string BuyerTaxID { get; set; }
        public string SuplierTaxID { get; set; }
        public decimal InvoiceCost { get; set; }
        
    }
}
