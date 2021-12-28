using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Routing;
using CoffeeShop.Dal;
using CoffeeShop.Models;

namespace CoffeeShop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index(List<user> users)
        {
            if (TempData["users"] != null)
                users = (List<user>) TempData["users"];
            if(users == null)
                users = (new UserDal()).Users.ToList<user>();
            
            List<coffee> coffeList = (new coffeeDal()).Coffee.ToList<coffee>();

            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "users", users },
                { "coffee", coffeList }
            };

            return View(dict);
        }

        [HttpPost]
        public ActionResult SearchUser()
        {
            String searchedValue = Request.Form["searchInput"];
            Console.WriteLine(searchedValue.ToString());
            UserDal userDal = new UserDal();
            List<user> users = (new UserDal()).Users.ToList<user>();
            users = (from x in userDal.Users where x.name.Contains(searchedValue) select x).ToList<user>();

            TempData["users"] = users;
            return RedirectToAction("Index");

        }

        

        public ActionResult EditCoffee()
        {
            string newPrice = Request.Form["newPrice"];
            string coffeeKey = null;
            if (Request.Form.AllKeys.Length != 0)
                coffeeKey = Request.Form.AllKeys[1];

            if (coffeeKey == null)
                return RedirectToAction("Index");

            coffeeDal cd = new coffeeDal();
            coffee updatedCoffee = cd.Coffee.Find(coffeeKey);
            cd.Coffee.Remove(updatedCoffee);
            cd.SaveChanges();
            
            if (!newPrice.Equals(""))
                updatedCoffee.price = newPrice;

            
            cd.Coffee.Add(updatedCoffee);
            cd.SaveChanges();


            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult AddUser()
        {
            UserDal usersd = new UserDal();
            string fn = Request.Form["fn"];
            string email = Request.Form["email"];
            string pass = Request.Form["pass"];
            string role = Request.Form["role"];
            user newUser = new user(fn, email, pass, role);
            usersd.Users.Add(newUser);
            usersd.SaveChanges();
            //List<user> users = usersd.Users.ToList<user>();

            //List<coffee> coffeList = (new coffeeDal()).Coffee.ToList<coffee>();

            //Dictionary<string, object> dict = new Dictionary<string, object>
            //{
            //    { "users", users },
            //    { "coffee", coffeList }
            //};


            return RedirectToAction("Index");
        }
       

    }
}