using Microsoft.EntityFrameworkCore;
using SupplierAPI.Constants;
using SupplierAPI.Domain.Entities;

namespace SupplierAPI.Infrastructure.DbConectionContext
{
    public class SuplierDbContext : DbContext
    {
        public SuplierDbContext(DbContextOptions dbContextOptions):base(dbContextOptions) { }

        public DbSet<Supplier> Suppliers { get; set; }

        //QueueMessages table
        public DbSet<QueueMessage> QueueMessages { get; set; }
    }
}
