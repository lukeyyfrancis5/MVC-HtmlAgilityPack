using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Ledger
    {
        public int LedgerId { get; set; }
        public DateTime Time { get; set; }

        public virtual List<Coin> CryptoCoins { get; set; }
    }
}