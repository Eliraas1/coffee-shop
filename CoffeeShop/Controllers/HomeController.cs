using CoffeeShop.Dal;
using CoffeeShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;


namespace CoffeeShop.Controllers
{
    public class HomeController : Controller
    {
        private drinksDal db = new drinksDal();
        private TblDal tableDB = new TblDal();
        private UserDal userDB = new UserDal();
        private TableOrderDal tableOrderDB = new TableOrderDal();
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
            d = db.Drink.AsEnumerable().OrderBy(pop => pop.popular).ToList();
            return d;

        }
        public List<Drink> DescByPop()
        {

            List<Drink> d = db.Drink.ToList<Drink>();
            d = db.Drink.OrderByDescending(pop => pop.price).ToList();
            return d;

        }
        public List<Drink> AscByPrice()
        {

            List<Drink> d = db.Drink.ToList<Drink>();
            d = db.Drink.AsEnumerable().OrderBy(pop => float.Parse(pop.price)).ToList();
            return d;

        }
        public List<Drink> DescByPrice()
        {

            List<Drink> d = db.Drink.ToList<Drink>();
            d = db.Drink.AsEnumerable().OrderByDescending(pop => float.Parse(pop.price)).ToList();
            return d;

        }

        [HttpPost]
        public ActionResult BookTable()
        {
            string dateRequest = Request.Form["date"];
            string time = Request.Form["time"];
            string dateParsedToString = dateRequest +' '+ time;
            string date = DateTime.Parse(dateParsedToString).ToString("dd/MM/yyyy HH:mm tt");

            string inOut = Request.Form["insideOutside"];
            bool isIn = false;
            if (inOut.Equals("Inside"))
                isIn = true;

            string numberOfSeats = Request.Form["numberOfSeats"];
            CheckAvailableAndBookOrder(numberOfSeats, isIn, date);
     
            return RedirectToAction("Index");
        }

        public void BookOrderTable(string date, int tid,string numberOfSeats)
        {
            
            string email = Session["email"].ToString();
            tableOrderDB.TableOrder.Add(new TableOrder(date, email, tid, int.Parse(numberOfSeats)));
            tableDB.SaveChanges();
        }

        public bool CheckIfAvailable(string date, int tid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["TableOrderDal"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("select * from users where date='" + date + "' AND tid='" + tid + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                Response.Write("<script>alert('Table is not available')</script>");
                return false;
            }
            return true;
            
        }

        public void CheckAvailableAndBookOrder(string numberOfSeats, bool isIn, string date)
        {
            List<Tbl> tableList = tableDB.tbls.AsEnumerable().Where(tb => (tb.amount >= int.Parse(numberOfSeats))).ToList();
            if (tableList.Count() == 0)
                return;

            //check if there is table inside with this numer of seats
            List<Tbl> tl = tableList.AsEnumerable().Where(tb => (tb.isIn == isIn)).ToList();
            if (tl.Count() == 0)
                return;

            //check if table already taken
            foreach (Tbl t in tl)
            {
                if (!CheckIfAvailable(date, t.tid))
                    continue;

                BookOrderTable(date, t.tid, numberOfSeats);
            }
        }
    }
}
/*       
public String Date { get; set; } /*DateTime.Now.ToString("dd/MM/yyyy HH:mm")	29/05/2015 05:50

public int Uid { get; set; }

public int Tid { get; set; }

public int NumberOfSeats { get; set; }
*/