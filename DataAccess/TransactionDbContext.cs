using Acmepay_ltd.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}

