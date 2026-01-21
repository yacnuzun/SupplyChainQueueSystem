using BuyerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuyerAPI.Infrastructure.Data.DbConnectionContext
{
    public class BuyerDbContext : DbContext
    {
        public BuyerDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Buyer> Buyers { get; set; }

    }
}
