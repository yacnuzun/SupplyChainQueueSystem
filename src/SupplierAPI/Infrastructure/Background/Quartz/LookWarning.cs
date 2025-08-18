using Microsoft.EntityFrameworkCore;
using Quartz;
using SupplierAPI.Infrastructure.DbConectionContext;

namespace SupplierAPI.Infrastructure.Background.Quartz
{
    public class LookWarning : IJob
    {
        private readonly SuplierDbContext _suplierDbContext;

        public LookWarning(SuplierDbContext suplierDbContext)
        {
            _suplierDbContext = suplierDbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var list = _suplierDbContext.QueueMessages.ToList();
            foreach (var item in list.Where(l => l.IsRead is not true).ToList())
            {
                Console.WriteLine($"{item.TermDate} tarihinde {item.BuyerTaxID} taxid numaralı firma tarafından {item.InvoiceNumber} numaralı fatura oluşturulmuştur.");

                item.IsRead = true;

                var updatedEntity = _suplierDbContext.Entry(item);
                updatedEntity.State = EntityState.Modified;
                _suplierDbContext.SaveChanges();

            }

        }
    }
}
