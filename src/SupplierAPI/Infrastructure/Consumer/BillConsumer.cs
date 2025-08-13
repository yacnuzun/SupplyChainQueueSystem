using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Events;
using SupplierAPI.Entities;
using SupplierAPI.Entities.DbConectionContext;

namespace SupplierAPI.Infrastructure.Consumer
{
    public class BillConsumer : IConsumer<BillEvent>
    {
        public async Task Consume(ConsumeContext<BillEvent> context)
        {

            using (var dbContext = new SuplierDbContext())
            {
                var addedEntity = dbContext.Entry(new QueueMessage
                {
                    QueueGUID = context.MessageId,
                    BuyerTaxID = context.Message.BuyerTaxID,
                    InvoiceCost = context.Message.InvoiceCost,
                    InvoiceNumber = context.Message.InovoiceNumber,
                    SuplierTaxID = context.Message.SuplierTaxID,
                    TermDate = context.Message.TermDate,
                    IsRead = false
                });
                addedEntity.State = EntityState.Added;
                dbContext.SaveChanges();
            }

        }
    }
}
