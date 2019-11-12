using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HarmonicaTabs.Models;
using MySql.Data.MySqlClient;
using HarmonicaTabs.Helpers;

namespace HarmonicaTabs.Controllers
{
    public class LoginController : Controller
    {

        private string errorMsg;
        private DatabaseConnector dbcon = new DatabaseConnector();

        // GET: Login
        public ActionResult Index()
        {
            return View(new Login());
        }

        [HttpPost]
        public ActionResult Index(string login, string password)
        {

            string loginType = "username";

            try
            {
                dbcon.openConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = dbcon.connection;
                cmd.CommandText = string.Format("SELECT password FROM logins WHERE {0}='" + login + "';", loginType);

                MySqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                if (BCrypt.Net.BCrypt.Verify(password, reader.GetString(0)))
                {
                    errorMsg = "Login successful";
                }
                else
                {
                    errorMsg = "Login failed";
                }

                dbcon.closeConnection();
            }
            catch (MySqlException e)
            {
                errorMsg = e.Message;
            }

            return View(new Login() { error = errorMsg });
        }
    }
}