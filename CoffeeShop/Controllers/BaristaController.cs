using CoffeeShop.Dal;
using CoffeeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Controllers
{
    public class BaristaController : Controller
    {
        private OrdersDal orders = new OrdersDal();


        // GET: Barista
        public ActionResult Index()
        {

            return View(orders.orders.ToList<Order>());
        }

        public ActionResult SearchOrders()
        {
            String searchedValue = Request.Form["searchInput"];

            List<Order> ord = orders.orders.ToList<Order>();
            ord = (from x in orders.orders where x.uid.Contains(searchedValue) select x).ToList<Order>();


            return PartialView("search", ord);
        }

        public List<Drink> OrdersPerOrder(Order o)
        {
            List<Drink> l = new List<Drink>();

            return l;
        }
    }
}