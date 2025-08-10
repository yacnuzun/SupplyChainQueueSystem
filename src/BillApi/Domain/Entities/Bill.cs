using Shared.Persistance.Entities;

namespace BillApi.Domain.Entities
{
    public class Bill : IEntity
    {
        public int BillID { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal InvoiceCost { get; set; }
        public string TermDate { get; set; }
        public string BuyerTaxID { get; set; }
        public string SuplierTaxID { get; set; }
        public Status InovoiceStatus { get; set; }
    }

    public enum Status
    {
        New,
        Usage,
        Paid
    }
}
