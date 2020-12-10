using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers
{
    public class HomeController : Controller
    {
        //[ResponseCache(CacheProfileName = "Home", Duration = 30)]
        public IActionResult Index()
        {
            ViewData["Title"] = "Benvenuto su MyCourse!";
            return View();
        }
    }
}
