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
         public ActionResult AscByPop()
        {
            List<Drink> d = db.Drink.ToList<Drink>();

            IEnumerable<Drink> Isort = d.OrderByDescending(drink => drink.popular);
            List<Drink> sort = Isort.ToList();
            return View("Menu", sort);
        }
        

    }
}