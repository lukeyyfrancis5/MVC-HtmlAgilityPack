using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication2.Models;


namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult ViewSnap(int id)
        {
           LedgerDBContext db = new LedgerDBContext();
            
            //   Ledger fullTable = new Ledger();
            //fullTable.CryptoCoins = (from Coin in db.Coins
            //    join Ledger in db.Ledgers
            //        on Coin.LedgerID equals Ledger.LedgerId
            //    where Coin.ID == Ledger.LedgerId
            //    select new Ledger()
            //    {
            //        CryptoCoins = Ledger.CryptoCoins,
            //        Time = Ledger.Time
            //    }).ToList();
            
            return View();
        }

        public ActionResult ViewSnaps()
        {
            ViewBag.Message = "See your snaps here !";
            using (var db = new LedgerDBContext())
            {
                List<DateTime> times = new List<DateTime>();

                foreach (var dbCoin in db.Coins)
                {
                    times.Add(dbCoin.Time);
                }

                Console.WriteLine(times);
                
                return View(times);
            }
        }

        public ActionResult CryptoData()
        {
            ViewBag.Message = "Your table page!";
            ViewBag.Time = DateTime.Now;

            using (var db = new LedgerDBContext())
            {
                                       
                var url = "https://coinmarketcap.com/";

                var htmlWeb = new HtmlWeb();

                var htmlDocument = new HtmlDocument();
                htmlDocument = htmlWeb.Load(url);

                //var Time = DateTime.Now;

                HtmlNodeCollection allRows = htmlDocument.DocumentNode.SelectNodes("//table[1]/tbody[1]/tr[*]");
                //var rowNumber = 0;
                List<Coin> currencyDataList = new List<Coin>();

                foreach (var row in allRows)
                {
                    // Console.WriteLine("Attempting to process row: " + rowNumber++);

                    var CoinSymbol = row.ChildNodes[3].ChildNodes[3].InnerText;
                    var CoinName = row.ChildNodes[3].ChildNodes[5].InnerText;
                    var CoinPrice = row.ChildNodes[7].InnerText;
                    
                    Coin coin = new Coin(CoinSymbol, CoinName, CoinPrice);
                    currencyDataList.Add(coin);
                }

                db.SaveChanges();
               
                /* Creates a date.time snapshot- this will be my future snapshot button
                var ledger = new Ledger
                {
                    CryptoCoins = currencyDataList,
                    Time = DateTime.Now
                }; 

                db.Ledgers.Add(ledger);
                db.SaveChanges();
                */

                Console.WriteLine();
                return View(currencyDataList);
            }
        }
    }
}