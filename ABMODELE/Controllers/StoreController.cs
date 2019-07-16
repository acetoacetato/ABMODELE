using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABMODELE.Models;

namespace ABMODELE.Controllers
{
    public class StoreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Store
        public ActionResult Index()
        {
            var categoria = db.Categoria.ToList();

           
            return View(categoria);
        }
        
        /*public ActionResult Browse (string categoria)
        {
            var CategoriaModel = db.Categoria.Include("nombre").Single(c=>c.nombre==categoria);
            return View(CategoriaModel);
        }*/
       
        public ActionResult Details (int id)
        {
            var producto = new Producto { Nombre = "Producto" + id };
            return View(producto);
        }
    }
}