using System.Data.Entity;

namespace WebApplication2.Models
{
    public class LedgerDBContext : DbContext
    {
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<Coin> Coins { get; set; }
    }
}