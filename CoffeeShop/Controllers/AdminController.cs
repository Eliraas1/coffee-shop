using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using CoffeeShop.Dal;
using CoffeeShop.Models;

namespace CoffeeShop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index(List<user> users = null)
        {
            if(users == null)
                users = (new UserDal()).Users.ToList<user>();
            
            return View(users);
        }

        [HttpPost]
        public ActionResult SearchUser()
        {
            String searchedValue = Request.Form["searchInput"];
            Console.WriteLine(searchedValue.ToString());
            UserDal userDal = new UserDal();
            List<user> users = (new UserDal()).Users.ToList<user>();
            users = (from x in userDal.Users where x.name.Contains(searchedValue) select x).ToList<user>();
            return View("Index", users);
            
        }

    }
}