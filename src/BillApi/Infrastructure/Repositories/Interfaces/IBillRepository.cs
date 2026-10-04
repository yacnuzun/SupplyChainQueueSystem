using BillApi.Domain.Entities;
using Shared.Persistance.Interfaces;

namespace BillApi.Infrastructure.Repositories.Interfaces
{
    public interface IBillRepository : IRepository<Bill>
    {
    }
}
