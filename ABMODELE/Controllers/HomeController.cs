using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABMODELE.Models;

namespace ABMODELE.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Estudiantes de la Pontificia Católica de Valparaiso";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Tu página de contacto.";

            return View();
        }

   
    }
}