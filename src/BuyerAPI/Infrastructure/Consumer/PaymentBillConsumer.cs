using MassTransit;
using Shared.Events;

namespace BuyerAPI.Infrastructure.Consumer
{
    public class PaymentBillConsumer : IConsumer<EarlyPaymentEvent>
    {

        public async Task Consume(ConsumeContext<EarlyPaymentEvent> context)
        {

            Console.WriteLine($"{context.Message.TermDate} tarihinde {context.Message.SuplierTaxID} taxid'li tedarikçi tarafından {context.Message.InovoiceNumber} numaralı fatura kullanıldı.");

        }
    }
}
