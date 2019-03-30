using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace testSqlSeverMVC.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // 
        // GET: /HelloWorld/Welcome/
        public IActionResult Welcome(string name, int numTimes=1)
        {
            ViewData["Mensaje"] = "Hola" + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }
    }
}