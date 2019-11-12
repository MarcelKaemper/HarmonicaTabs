using System.Web.Mvc;
using HarmonicaTabs.Models;
using System.Configuration;
using MySql.Data.MySqlClient;
using System;

namespace HarmonicaTabs.Controllers
{
    public class RegisterController : Controller
    {

        private MySqlConnection connection;
        private string errorMsg = "";

        // GET: Register
        [HttpGet]
        public ActionResult Index()
        {
            return View(new Register());
        }

        [HttpPost]
        public ActionResult Index(string username, string email, string password, string confirmPassword)
        {

            // If all fields filled out
            if(!username.Equals("") &&
               !email.Equals("") &&
               !password.Equals("") &&
               !confirmPassword.Equals("")) 
            {
                // Password has at least 8 characters
                if(password.Length >= 8)
                {
                    // Passwords are equal
                    if (password.Equals(confirmPassword))
                    {
                        // Username length greater or equal to 4
                        if(username.Length >= 4)
                        {
                            try
                            {
                                openConnection();

                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = connection;
                                string sql = "INSERT INTO logins (uuid,email,username,password) VALUES ('{0}', '{1}', '{2}', '{3}')";
                                cmd.CommandText = string.Format(sql, generateUUID(), email, username, BCrypt.Net.BCrypt.HashPassword(password, 10));
                                cmd.ExecuteNonQuery();
                                
                                closeConnection();
                            } catch (MySqlException e)
                            {
                                errorMsg = e.Message;
                            }
                        } else
                        {
                            errorMsg = "Username mustBCrypt.Net.BCrypt.HashPassword be at least 4 characters long";
                        }
                    }
                    else
                    {
                        errorMsg = "Passwords do not match";
                    }
                } else
                {
                    errorMsg = "Password must be at least 8 characters long";
                }
            }
            
            return View(new Register() { error=errorMsg});
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

        private string generateUUID()
        {
            string uuid = "";
            Random random = new Random();

            while(uuid.Length < 16) {
                int rand = random.Next(0, 3);
                if (rand == 0)
                {
                    uuid += random.Next(0, 10);
                } else if (rand == 1)
                {
                    uuid += (char)('a' + random.Next(0, 26));
                } else
                {
                    if (random.Next(0, 4) == 0)
                    {
                        int r = random.Next(0, 3);
                        uuid += (r == 0) ? "!" : (r == 1) ? "?" : "_";
                    }
                }
            }
            return uuid;
        }
    }
}