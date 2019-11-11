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

        // GET: Register
        [HttpGet]
        public ActionResult Index()
        {
            var register = new Register();

            return View(register);
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
                                //string sql = "Name:{0} {1}, Location:{2}, Age:{3}";
                                //string msg = string.Format(s, "Suresh", "Dasari", "Hyderabad", 32);

                                openConnection();
                                
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = connection;
                                cmd.CommandText = string.Format("INSERT INTO logins (uuid,email,username,password) " +
                                    "                            VALUES ('{0}', '{1}', '{2}', '{3}')"
                                                                        ,generateUUID(),email, username, password);
                                cmd.ExecuteNonQuery();
                                closeConnection();
                            } catch (MySqlException e)
                            {
                                System.Diagnostics.Debug.WriteLine(e.ToString());
                            }
                        }
                    }
                }
            }

            var register = new Register();
            
            return View(register);
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