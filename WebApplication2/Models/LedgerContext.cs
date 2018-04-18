using System.Data.Entity;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    class LedgerContext : DbContext
    {
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<Coin> Coins { get; set; }
    }
}