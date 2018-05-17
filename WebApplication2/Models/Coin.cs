using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Coin
    {
        public int ID { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        
        
        


        public Coin(string symbol, string name, string price)
        {
            Symbol = symbol;
            Name = name;
            Price = Double.Parse(price,NumberStyles.Currency);
        }
    }
}