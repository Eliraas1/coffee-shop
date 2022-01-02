﻿using System.Data.SqlClient;
using System.Web.Mvc;
using System.Configuration;

namespace CoffeeShop.Controllers
{
    public class LogRegController : Controller
    {
        // GET: LogReg
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Log()
        {
            var email = Request.Form["email"];
            var pass = Request.Form["pass"];
            string strcon = ConfigurationManager.ConnectionStrings["UserDal"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand("select * from users where email='" + email + "' AND password='" + pass + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                Response.Write("<script>alert('user IsAuthenticated successfully!')</script>");
                while (dr.Read())
                {

                    Session["name"] = dr.GetValue(0).ToString();
                    Session["email"] = dr.GetValue(1).ToString();
                    Session["pass"] = dr.GetValue(2).ToString();
                    Session["role"] = dr.GetValue(3).ToString();
                }
                return RedirectToAction("Index", "Home");

            }
            else
            {
                Response.Write("<script>alert('user not matched!')</script>");
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult Reg()
        {
            var name = Request.Form["name"];
            string role = "customer";
            var email = Request.Form["email"];
            var pass = Request.Form["pass"];
            int age = int.Parse(Request.Form["age"]);
            string vip = Request.Form["VipOrStantard"];
            bool isVip = false;
            if (vip.Equals("Vip"))
                isVip = true;

            SqlConnection con = new SqlConnection("Data Source=HIZO-PC;Initial Catalog=users;Integrated Security=TrueS");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[users]
           ([name]
           ,[email]
           ,[password]
           ,[role]
           ,[age]
            ,[isVip])
            VALUES
           ('" + name + "' ,'" + email + "' ,'" + pass + "','" + role + "','" + age + "','" + isVip + "')", con);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('user is registered successfully!')</script>");
            if (Request.IsAuthenticated)
                Response.Write("<script>alert('user IsAuthenticated successfully!')</script>");
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session["name"] = null;
            Session["email"] = null;
            Session["pass"] = null;
            Session["role"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}




