﻿using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public class Ledger
    {
        public int LedgerId { get; set; }
        public DateTime Time  { get; set; }
        public List<Coin> Coins { get; set; }
    }
}