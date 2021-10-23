using AspMvcCustomAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AspMvcCustomAuthentication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(RegisterModel rmm)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='E:\\Aplicatii VS\\AspMvcCustomAuthentication\\AspMvcCustomAuthentication\\App_Data\\AspMvcCustomAuthentication.mdf';Integrated Security=True");//connection
            string SqlQuery = "select Email,Password from RegisteredUsers where Email=@Email and Password=@Password";
            con.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
            cmd.Parameters.AddWithValue("@Email", rmm.Email);
            cmd.Parameters.AddWithValue("@Password", rmm.Password);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Session["Email"] = rmm.Email.ToString();
                return RedirectToAction("Welcome");
            }
            else
            {
                ViewData["Message"] = "User Login Details Failed!!";
            }
            if (rmm.Email.ToString() != null)
            {
                Session["Email"] = rmm.Email.ToString();
            }
            con.Close();
            return View();
        }

        [HttpGet]
        public ActionResult Welcome()
        {
            RegisterModel user = new RegisterModel();
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='E:\\Aplicatii VS\\AspMvcCustomAuthentication\\AspMvcCustomAuthentication\\App_Data\\AspMvcCustomAuthentication.mdf';Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetUserDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<RegisterModel> userlist = new List<RegisterModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        RegisterModel uobj = new RegisterModel();
                        uobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        uobj.FirstName = ds.Tables[0].Rows[i]["FirstName"].ToString();
                        uobj.LastName = ds.Tables[0].Rows[i]["LastName"].ToString();
                        uobj.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                        uobj.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                        uobj.PhoneNumber = ds.Tables[0].Rows[i]["Phone"].ToString();
                        uobj.SecurityAnwser = ds.Tables[0].Rows[i]["SecurityAnwser"].ToString();
                        uobj.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();

                        userlist.Add(uobj);

                    }
                    user.RegisteredUsersInfo = userlist;
                }
                con.Close();

            }
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}