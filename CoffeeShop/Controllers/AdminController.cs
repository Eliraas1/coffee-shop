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
            
            List<coffee> coffeList = (new coffeeDal()).Coffee.ToList<coffee>();

            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("users",users);
            dict.Add("coffee", coffeList);

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

            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("users", users);

            return View("Index", dict);
            
        }

        public ActionResult editPage()
        {
            List<coffee> coffeList = (new coffeeDal()).Coffee.ToList<coffee>();
            return View("EditCoffee", coffeList);
        }


        public ActionResult EditCoffee()
        {

            string newPrice = Request.Form["newPrice"];
            string key = null;
            if (Request.Form.AllKeys.Length == 0)
                key = null;
            else
                key = Request.Form.AllKeys[2];

            string newName = Request.Form["newName"];

            if (key == null)
                return View("EditCoffee");

            coffeeDal cd = new coffeeDal();
            coffee removed = cd.Coffee.Find(key);
            cd.Coffee.Remove(removed);
            cd.SaveChanges();
            if (!newName.Equals(""))
                removed.name = newName;

            if (!newPrice.Equals(""))
                removed.price = newPrice;

            
            cd.Coffee.Add(removed);
            cd.SaveChanges();
            return View("EditCoffee");
        }

        public void sqlCmd(string value,string key)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-U2T54MF;database=CoffeProj;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(@"update[dbo].[coffee]
                                                set name = @value
                                                where name = @key", con);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }



    }
}