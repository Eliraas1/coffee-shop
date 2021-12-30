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

        public ActionResult Menu(List<Drink> sort = null)
        {
            if (sort == null)
                sort = db.Drink.ToList<Drink>();

            return View(sort);
        }
        public ActionResult Sort()
        {
            string userSelection = Request.Form["sortSelect"];
            List<Drink> drinks = db.Drink.ToList<Drink>();

            return View("Menu", sortActionHelper(userSelection, drinks));
        }

        //Sort Helpers
        public List<Drink> sortActionHelper(string select, List<Drink> drink)
        {
            switch(select){
                case "least-popular": return sortByPopulareAsc(drink);
                case "most-popular": return sortByPopulareDesc(drink);
                case "price-asc": return sortByPriceAsc(drink);
                case "price-desc": return sortByPriceDesc(drink);
                case "name-asc": return sortByNameAsc(drink);
                case "name-desc": return sortByNameDesc(drink);
                case "in-stock": return sortByStock(drink);
                default: return sortByPopulareAsc(drink);
            }
        }
        public List<Drink> sortByPriceAsc(List<Drink> drinks)
        {
            return drinks.OrderBy(drink => drink.price).ToList();
        }
        public List<Drink> sortByPriceDesc(List<Drink> drinks)
        {
            return drinks.OrderByDescending(drink => drink.price).ToList();
        }
        public List<Drink> sortByPopulareDesc(List<Drink> drinks)
        {
            return drinks.OrderByDescending(drink => drink.popular).ToList();
        }
        public List<Drink> sortByPopulareAsc(List<Drink> drinks)
        {
            return drinks.OrderBy(drink => drink.popular).ToList();
        }
        public List<Drink> sortByNameAsc(List<Drink> drinks)
        {
            return drinks.OrderBy(drink => drink.name).ToList();
        }
        public List<Drink> sortByNameDesc(List<Drink> drinks)
        {
            return drinks.OrderByDescending(drink => drink.name).ToList();
        }
        public List<Drink> sortByStock(List<Drink> drinks)
        {
            return drinks.OrderByDescending(drink => drink.amount).ToList().Where(d=>d.amount>0).ToList();
        }
    }
}