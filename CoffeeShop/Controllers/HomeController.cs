using CoffeeShop.Dal;
using CoffeeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CoffeeShop.Controllers
{
    public class HomeController : Controller
    {
        private drinksDal db = new drinksDal();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchItems()
        {
            String searchedValue = Request.Form["searchInput"];
            List<Drink> drinks = db.Drink.ToList<Drink>();
            drinks = (from x in db.Drink where x.name.Contains(searchedValue) select x).ToList<Drink>();
            TempData["SearchItems"] = drinks;
            return RedirectToAction("Menu");

        }

        public ActionResult Menu(string sort)
        {
            ViewBag.sort = db.Drink.ToList<Drink>();

            string s = Request.Form["sortSelect"];

            if (TempData["SearchItems"] != null)
                ViewBag.sort = TempData["SearchItems"];
            else if (s == null)
                return View();
            else if (s.Equals("in-stock"))
                ViewBag.sort = DescByAmount();
            else if (s.Equals("name-asc"))
                ViewBag.sort = AscByName();
            else if (s.Equals("name-desc"))
                ViewBag.sort = DescByName();
            else if (s.Equals("least-popular"))
                ViewBag.sort = AscByPop();
            else if (s.Equals("most-popular"))
                ViewBag.sort = DescByPop();
            else if (s.Equals("price-asc"))
                ViewBag.sort = AscByPrice();
            else if (s.Equals("price-desc"))
                ViewBag.sort = DescByPrice();
            return View();
        }

        public List<Drink> AscByAmount()
        {

            List<Drink> d = db.Drink.ToList<Drink>();
            d = db.Drink.OrderBy(pop => pop.amount).ToList<Drink>();
            return d;

        }
        public List<Drink> DescByAmount()
        {

            List<Drink> d = db.Drink.ToList<Drink>();
            d = db.Drink.OrderByDescending(pop => pop.amount).ToList<Drink>();
            return d;

        }
        public List<Drink> AscByName()
        {

            List<Drink> d = db.Drink.ToList<Drink>();
            d = db.Drink.OrderBy(pop => pop.name).ToList<Drink>();
            return d;

        }
        public List<Drink> DescByName()
        {

            List<Drink> d = db.Drink.ToList<Drink>();
            d = db.Drink.OrderByDescending(pop => pop.name).ToList<Drink>();
            return d;

        }
        public List<Drink> AscByPop()
        {

            List<Drink> d = db.Drink.ToList<Drink>();
            d = db.Drink.OrderBy(pop => pop.popular).ToList<Drink>();
            return d;

        }
        public List<Drink> DescByPop()
        {

            List<Drink> d = db.Drink.ToList<Drink>();
            d = db.Drink.OrderByDescending(pop => pop.popular).ToList<Drink>();
            return d;

        }
        public List<Drink> AscByPrice()
        {

            List<Drink> d = db.Drink.ToList<Drink>();
            d = db.Drink.AsEnumerable().OrderBy(pop => Convert.ToDouble(pop.price)).ToList<Drink>();
            return d;

        }
        public List<Drink> DescByPrice()
        {

            List<Drink> d = db.Drink.ToList<Drink>();
            d = db.Drink.AsEnumerable().OrderByDescending(pop => Convert.ToDouble(pop.price)).ToList<Drink>();
            return d;

        }

    }
}