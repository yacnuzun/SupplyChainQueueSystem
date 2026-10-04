using BillApi.Domain.Entities;
using Shared.Abstract;

namespace BillApi.Dto_s
{
    public class CreateBillDTO: IDTO
    {
        public string TermDate { get; set; }
        public string BuyerTaxID { get; set; }
        public string SuplierTaxID { get; set; }
        public decimal InvoiceCost { get; set; }
        public static CreateBillDTO GetViewModel(Bill model)
        {
            return new CreateBillDTO
            {
                TermDate = model.TermDate,
                InvoiceCost = model.InvoiceCost,
                BuyerTaxID = model.BuyerTaxID,
                SuplierTaxID = model.SuplierTaxID,
            };
        }
    }

}
