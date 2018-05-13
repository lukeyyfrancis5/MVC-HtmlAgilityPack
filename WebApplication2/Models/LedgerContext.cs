using System.Data.Entity;

namespace WebApplication2.Models
{
    public class LedgerDBContext : DbContext
    {
        public DbSet<Coin> Coins { get; set; }
    }
}