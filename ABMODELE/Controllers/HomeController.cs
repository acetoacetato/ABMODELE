using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABMODELE.Models;
using ABMODELE.Models.ViewModel;

namespace ABMODELE.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var produc = db.Producto.Where(prod => prod.Destacado == true).ToList();
            var cat = db.Categoria.ToList();
            var home = new HomeViewModel()
            {
                categorias = cat,
                productos = produc
            };
            
            return View(home);
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