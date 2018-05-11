using HtmlAgilityPack;
using System;
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
            
               /* Ledger fullTable = new Ledger();
                fullTable.CryptoCoins = (from Coin in db.CryptoCoins
                                   Join) */
            
            return View();
        }

        public ActionResult ViewSnaps()
        {
            ViewBag.Message = "See your snaps here !";
            using (var db = new LedgerDBContext())
            {
                List<DateTime> times = new List<DateTime>();

                foreach (var dbLedger in db.Ledgers)
                {
                    times.Add(dbLedger.Time);
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
                // Creates a date.time snapshot
                var ledger = new Ledger
                {
                    CryptoCoins = currencyDataList,
                    Time = DateTime.Now
                };

                db.Ledgers.Add(ledger);
                db.SaveChanges();

                Console.WriteLine();
                return View(currencyDataList);
            }
        }
    }
}