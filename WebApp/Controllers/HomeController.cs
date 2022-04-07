using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var userid = Convert.ToInt32(User.Identity.Name);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
