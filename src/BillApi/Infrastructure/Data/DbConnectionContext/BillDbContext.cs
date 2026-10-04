using BillApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillApi.Infrastructure.Data.DbConnectionContext
{
    public class BillDbContext : DbContext
    {
        public BillDbContext(DbContextOptions builder) : base(builder)
        {
        }

        public DbSet<Bill> Bills { get; set; }
    }
}
