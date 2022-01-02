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
        private Dictionary<Drink, int> cartDictionary = new Dictionary<Drink, int>();
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

        public ActionResult AddToCart()
        {
            
            Dictionary<Drink, int> dict = (Dictionary<Drink, int>)Session["CartDict"];

            int p = int.Parse(Request.Form["prod"]);
            Drink d = db.Drink.Find(p);
            int val = 0;
            
            if((ContainDrink(d, dict))!=null && dict[(ContainDrink(d,dict))] == d.amount)
            {
                return PartialView("ProdCount", -1); //
            }


            Session["CartCount"] = int.Parse(Session["CartCount"].ToString()) + 1;
            ((List<Drink>)Session["CartProd"]).Add(d);
            int count = ((List<Drink>)Session["CartProd"]).Count;


            addDrinkToDictionary(d,dict);
            ViewBag.Discount = calcDiscount(dict);
            return PartialView("ProdCount",count);
        }

        public ActionResult Cart() {
            Dictionary<Drink, int> dict = (Dictionary<Drink, int>)Session["CartDict"];
            ViewBag.Discount = calcDiscount(dict);

            return View();

        }

        /*    Cart Help Functions      */
        public bool isContainDrink(Dictionary<Drink, int> dict, Drink drink)
        {
            foreach(Drink d in dict.Keys)
            {
                if (d.id == drink.id)
                {
                    dict[d]++;
                    return true;
                }
            }
            return false;
        }

        public Drink ContainDrink(Drink drink, Dictionary<Drink, int> dict)
        {
            if (dict.Count() <= 0)
                return null;
            
            foreach (Drink d in dict.Keys)
            {
                if (d.id == drink.id)
                {
                    return d;
                }
            }
            return null;
        }

        public void addDrinkToDictionary(Drink d, Dictionary<Drink, int> dict)
        {
            if (!isContainDrink(dict, d))
                dict.Add(d, 1);

        }

        public float calcDiscount(Dictionary<Drink, int> dict)
        {
            float discount = 0;
            if (!isVip())
                return discount;

            int count = 0;
            //check if he is vip and give him discount every 10 coffees
            foreach (Drink drink in dict.Keys)
            {
                if (drink.isAlcohol)
                    continue;

                
                count += dict[drink];
                if((count/10) > 0)
                    discount += (count/10) * float.Parse(drink.price); 

            }
            
            return discount;
        }

        public bool isVip()
        {    
            if (Session["email"] == null)
                return false;

            return userDB.Users.Find(int.Parse(Session["Uid"].ToString())).isVip;
        }
        /*                             */

        /***********************sorting menu helper functions************************/
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
        /**************************************************************************/

        /***********************booking table and  helper functions************************/
        [HttpPost]
        public ActionResult BookTable()
        {
            string dateRequest = Request.Form["date"];
            string time = Request.Form["time"];
            string dateParsedToString = fixDate(dateRequest) + ' ' + time;
            string date = DateTime.Parse(dateParsedToString).ToString("dd/MM/yyyy HH:mm");

            string inOut = Request.Form["insideOutside"];
            bool isIn = false;
            if (inOut.Equals("Inside"))
                isIn = true;

            string numberOfSeats = Request.Form["numberOfSeats"];
            CheckAvailableAndBookOrder(numberOfSeats, isIn, date);

            return RedirectToAction("Index");
        }

        public string fixDate(string date)
        {
            string[] seperateDate = date.Split('/');
            if (seperateDate[0].Count() == 1)
                seperateDate[0] = '0' + seperateDate[0];
            if (seperateDate[1].Count() == 1)
                seperateDate[1] = '0' + seperateDate[1];

            //swap mm/dd to dd/mm
            string temp = seperateDate[0];
            seperateDate[0] = seperateDate[1];
            seperateDate[1] = temp;

            return String.Join("/", seperateDate);
        }

        public void BookOrderTable(string date, int tid, string numberOfSeats)
        {
            //check if user connected, if not its a guest and have a name field in form
            string name = "";
            if (Session["email"] == null)
                name = "Guest " + Request.Form["Name"];
            else
            {
                user us = userDB.Users.Find(Session["Uid"]);
                name = us.role + " " + us.name;
            }


            TableOrder tableOrder = new TableOrder(date, name, tid, int.Parse(numberOfSeats));
            tableOrderDB.TableOrder.Add(tableOrder);
            tableOrderDB.SaveChanges();
        }

        public bool CheckIfAvailable(string date, int tid)
        {
            string strcon = ConfigurationManager.ConnectionStrings["TableOrderDal"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("select * from TableOrder where date='" + date + "' AND tid='" + tid + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                Response.Write("<script>alert('Table is not available')</script>");
                return false;
            }
            return true;

        }

        public void CheckAvailableAndBookOrder(string numberOfSeats, bool isIn, string date, string name = null)
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
                if (CheckIfAvailable(date, t.tid))
                {
                    BookOrderTable(date, t.tid, numberOfSeats);
                    return;
                }
            }
        }
        /**************************************************************************/
    }
}