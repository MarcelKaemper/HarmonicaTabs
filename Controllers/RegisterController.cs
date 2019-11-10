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
        public ActionResult Register()
        {
            var register = new Register();

            return View(register);
        }

        [HttpPost]
        public ActionResult Register(string username, string email, string password, string confirmPassword)
        {
            try
            {
                openConnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM test";
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                System.Diagnostics.Debug.WriteLine(reader.GetString(0));
                reader.Close();
            } catch (MySqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
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