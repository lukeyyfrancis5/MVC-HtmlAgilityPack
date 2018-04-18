using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewSnaps()
        {
            ViewBag.Message = "See your snaps here !";

            return View();
        }

        public ActionResult ScrapeActionName()
        {
            ViewBag.Message = "Your table page!";
            ViewBag.Time = DateTime.Now;

            using (var db = new LedgerContext())
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

                var ledger = new Ledger
                {
                    Coins = currencyDataList
                };

                db.Ledgers.Add(ledger);
                db.SaveChanges();
                return View(currencyDataList);
            }

            return HttpNotFound();
        }
    }
}