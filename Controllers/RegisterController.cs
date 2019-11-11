using System.Web.Mvc;
using HarmonicaTabs.Models;

using System.Configuration;
using MySql.Data.MySqlClient;

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
                                openConnection();
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = connection;
                                cmd.CommandText = "SELECT * FROM test";
                                MySqlDataReader reader = cmd.ExecuteReader();
                                reader.Read();
                                reader.Close();
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
    }
}