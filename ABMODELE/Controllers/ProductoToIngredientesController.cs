using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ABMODELE.Models;

namespace ABMODELE.Controllers
{
    public class ProductoToIngredientesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: ProductoToIngredientes
        public ActionResult Index()
        {
            var productoToIngrediente = db.ProductoToIngrediente.Include(p => p.IngredienteModel).Include(p => p.ProductoModel);
            return View(productoToIngrediente.ToList());
        }

        // GET: ProductoToIngredientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoToIngrediente productoToIngrediente = db.ProductoToIngrediente.Find(id);
            if (productoToIngrediente == null)
            {
                return HttpNotFound();
            }
            return View(productoToIngrediente);
        }

        // GET: ProductoToIngredientes/Create
        public ActionResult Create()
        {
            ViewBag.IngredienteId = new SelectList(db.IngredienteModel, "IngredienteId", "Tipo");
            ViewBag.ProductoId = new SelectList(db.ProductoModel, "ProductoId", "Nombre");
            return View();
        }

        // POST: ProductoToIngredientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductoId,IngredienteId,CantidadProducto")] ProductoToIngrediente productoToIngrediente)
        {
            if (ModelState.IsValid)
            {
                db.ProductoToIngrediente.Add(productoToIngrediente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IngredienteId = new SelectList(db.IngredienteModel, "IngredienteId", "Tipo", productoToIngrediente.IngredienteId);
            ViewBag.ProductoId = new SelectList(db.ProductoModel, "ProductoId", "Nombre", productoToIngrediente.ProductoId);
            return View(productoToIngrediente);
        }

        // GET: ProductoToIngredientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoToIngrediente productoToIngrediente = db.ProductoToIngrediente.Find(id);
            if (productoToIngrediente == null)
            {
                return HttpNotFound();
            }
            ViewBag.IngredienteId = new SelectList(db.IngredienteModel, "IngredienteId", "Tipo", productoToIngrediente.IngredienteId);
            ViewBag.ProductoId = new SelectList(db.ProductoModel, "ProductoId", "Nombre", productoToIngrediente.ProductoId);
            return View(productoToIngrediente);
        }

        // POST: ProductoToIngredientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductoId,IngredienteId,CantidadProducto")] ProductoToIngrediente productoToIngrediente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productoToIngrediente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IngredienteId = new SelectList(db.IngredienteModel, "IngredienteId", "Tipo", productoToIngrediente.IngredienteId);
            ViewBag.ProductoId = new SelectList(db.ProductoModel, "ProductoId", "Nombre", productoToIngrediente.ProductoId);
            return View(productoToIngrediente);
        }

        // GET: ProductoToIngredientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoToIngrediente productoToIngrediente = db.ProductoToIngrediente.Find(id);
            if (productoToIngrediente == null)
            {
                return HttpNotFound();
            }
            return View(productoToIngrediente);
        }

        // POST: ProductoToIngredientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductoToIngrediente productoToIngrediente = db.ProductoToIngrediente.Find(id);
            db.ProductoToIngrediente.Remove(productoToIngrediente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
