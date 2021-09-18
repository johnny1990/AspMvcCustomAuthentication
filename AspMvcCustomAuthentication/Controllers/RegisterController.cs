using AspMvcCustomAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspMvcCustomAuthentication.Controllers
{
    public class RegisterController : Controller
    {
        //public string value = "";

        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegisterModel e)
        {
            if (Request.HttpMethod == "POST")
            {
                RegisterModel er = new RegisterModel();
                using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='E:\\Aplicatii VS\\AspMvcCustomAuthentication\\AspMvcCustomAuthentication\\App_Data\\AspMvcCustomAuthentication.mdf';Integrated Security=True"))//connection
                {
                    using (SqlCommand cmd = new SqlCommand("sp_RegisterUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", e.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", e.LastName);
                        cmd.Parameters.AddWithValue("@Password", e.Password);
                        cmd.Parameters.AddWithValue("@Gender", e.Gender);
                        cmd.Parameters.AddWithValue("@Email", e.Email);
                        cmd.Parameters.AddWithValue("@Phone", e.PhoneNumber);
                        cmd.Parameters.AddWithValue("@SecurityAnwser", e.SecurityAnwser);
                        cmd.Parameters.AddWithValue("@Status", "INSERT");
                        con.Open();
                        ViewData["result"] = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return View();
        }
    }
}