using System.Data.Entity;

namespace WebApplication2.Models
{
    class LedgerContext : DbContext
    {
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<Coin> Coins { get; set; }
    }
}