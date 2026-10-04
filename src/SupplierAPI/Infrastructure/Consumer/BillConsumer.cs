using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Events;
using SupplierAPI.Domain.Entities;
using SupplierAPI.Infrastructure.DbConectionContext;

namespace SupplierAPI.Infrastructure.Consumer
{
    public class BillConsumer : IConsumer<BillEvent>
    {
        private readonly SuplierDbContext _suplierDbContext;

        public BillConsumer(SuplierDbContext suplierDbContext)
        {
            _suplierDbContext = suplierDbContext;
        }

        public async Task Consume(ConsumeContext<BillEvent> context)
        {

            var addedEntity = _suplierDbContext.Entry(new QueueMessage
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
            _suplierDbContext.SaveChanges();

        }

        public override bool Equals(object? obj)
        {
            return obj is BillConsumer consumer &&
                   EqualityComparer<SuplierDbContext>.Default.Equals(_suplierDbContext, consumer._suplierDbContext);
        }
    }
}
