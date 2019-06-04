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
    public class ProductoPersonalizadoesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: ProductoPersonalizadoes
        public ActionResult Index()
        {
            return View(db.ProductoPersonalizado.ToList());
        }

        // GET: ProductoPersonalizadoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoPersonalizado productoPersonalizado = db.ProductoPersonalizado.Find(id);
            if (productoPersonalizado == null)
            {
                return HttpNotFound();
            }
            return View(productoPersonalizado);
        }

        // GET: ProductoPersonalizadoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoPersonalizadoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idOrden,idProducto")] ProductoPersonalizado productoPersonalizado)
        {
            if (ModelState.IsValid)
            {
                db.ProductoPersonalizado.Add(productoPersonalizado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productoPersonalizado);
        }

        // GET: ProductoPersonalizadoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoPersonalizado productoPersonalizado = db.ProductoPersonalizado.Find(id);
            if (productoPersonalizado == null)
            {
                return HttpNotFound();
            }
            return View(productoPersonalizado);
        }

        // POST: ProductoPersonalizadoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idOrden,idProducto")] ProductoPersonalizado productoPersonalizado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productoPersonalizado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productoPersonalizado);
        }

        // GET: ProductoPersonalizadoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoPersonalizado productoPersonalizado = db.ProductoPersonalizado.Find(id);
            if (productoPersonalizado == null)
            {
                return HttpNotFound();
            }
            return View(productoPersonalizado);
        }

        // POST: ProductoPersonalizadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductoPersonalizado productoPersonalizado = db.ProductoPersonalizado.Find(id);
            db.ProductoPersonalizado.Remove(productoPersonalizado);
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
