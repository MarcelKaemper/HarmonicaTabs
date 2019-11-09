using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HarmonicaTabs.Models;

namespace HarmonicaTabs.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Register()
        {
            var register = new Register();

            return View(register);
        }
    }
}