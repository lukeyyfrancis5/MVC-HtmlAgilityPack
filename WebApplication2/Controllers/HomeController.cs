using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication2.Models;
using System.Data.Entity;


namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        
        //Need to properly dispose LedgerDBContext object
        public ActionResult ViewSnap(int id)
        {
            using (var db = new LedgerDBContext())
            {
                var shot = db.Ledgers.Include(l => l.CryptoCoins).FirstOrDefault(x => x.LedgerId == id);
                return View(shot);
            }
        }

        public ActionResult ViewSnaps()
        {
            ViewBag.Message = "See your snaps here !";

            using (var db = new LedgerDBContext())
            {
                return View(db.Ledgers.ToList());
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
                DateTime time = DateTime.Now;
                foreach (var row in allRows)
                {
                    // Console.WriteLine("Attempting to process row: " + rowNumber++);

                    var CoinSymbol = row.ChildNodes[3].ChildNodes[3].InnerText;
                    var CoinName = row.ChildNodes[3].ChildNodes[5].InnerText;
                    var CoinPrice = row.ChildNodes[7].InnerText;
                    
                    Coin coin = new Coin(CoinSymbol, CoinName, CoinPrice);
                    currencyDataList.Add(coin);
                }
              
               
                // Creates a date.time snapshot- this will be my future snapshot button
                var ledger = new Ledger
                {
                    CryptoCoins = currencyDataList,
                    Time = time
                };

                db.Ledgers.Add(ledger);
                db.SaveChanges();

                Console.WriteLine();
                return View(currencyDataList);
            }
        }
    }
}