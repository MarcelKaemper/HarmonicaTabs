using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HarmonicaTabs.Models;

namespace HarmonicaTabs.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Test()
        {
            var test = new Test() { Id = 1 };

            return View(test);
        }
    }
}