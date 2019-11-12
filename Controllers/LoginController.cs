using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HarmonicaTabs.Models;
using MySql.Data.MySqlClient;

namespace HarmonicaTabs.Controllers
{
    public class LoginController : Controller
    {

        private MySqlConnection connection;
        private string errorMsg;

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
                openConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                //cmd.CommandText = string.Format(sql, generateUUID(), email, username, BCrypt.Net.BCrypt.HashPassword(password, 10));
                cmd.CommandText = string.Format("SELECT password FROM logins WHERE {0}='" + login + "';", loginType);

                MySqlDataReader reader;
                reader = cmd.ExecuteReader();

                reader.Read();
                if (BCrypt.Net.BCrypt.Verify(password, reader.GetString(0)))
                {
                    errorMsg = "Login successful";
                }
                else
                {
                    errorMsg = "Login failed";
                }

                closeConnection();
            }
            catch (MySqlException e)
            {
                errorMsg = e.Message;
            }

            return View(new Login() { error = errorMsg });
        }
        private void openConnection()
        {
            connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString);
            connection.Open();
        }

        private void closeConnection()
        {
            connection.Close();
        }
    }
}