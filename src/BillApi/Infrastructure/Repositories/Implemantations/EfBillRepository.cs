using BillApi.Domain.Entities;
using BillApi.Infrastructure.Data.DbConnectionContext;
using BillApi.Infrastructure.Repositories.Interfaces;
using Shared.Persistance.Implamantations;
using Shared.Persistance.Interfaces;

namespace BillApi.Infrastructure.Repositories.Implemantations
{
    public class EfBillRepository : EfRepository<Bill, BillDbContext>, IBillRepository
    {
        public EfBillRepository(BillDbContext context) : base(context)
        {
        }
    }
}
