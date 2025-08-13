using Shared.Persistance.Entities;

namespace SupplierAPI.Domain.Entities
{
    public class QueueMessage : IEntity
    {
        public int QueueMessageID { get; set; }
        public Guid? QueueGUID { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal InvoiceCost { get; set; }
        public string TermDate { get; set; }
        public string BuyerTaxID { get; set; }
        public string SuplierTaxID { get; set; }
        public bool IsRead { get; set; }
    }
}
