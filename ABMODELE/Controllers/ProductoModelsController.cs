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
    public class ProductoModelsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: ProductoModels
        public ActionResult Index()
        {
            return View(db.ProductoModel.ToList());
        }

        // GET: ProductoModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoModel productoModel = db.ProductoModel.Find(id);
            if (productoModel == null)
            {
                return HttpNotFound();
            }
            return View(productoModel);
        }

        // GET: ProductoModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoModels/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductoId,Nombre,Precio,ConJuna,FechaPreparacion")] ProductoModel productoModel)
        {
            if (ModelState.IsValid)
            {
                db.ProductoModel.Add(productoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productoModel);
        }

        // GET: ProductoModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoModel productoModel = db.ProductoModel.Find(id);
            if (productoModel == null)
            {
                return HttpNotFound();
            }
            return View(productoModel);
        }

        // POST: ProductoModels/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductoId,Nombre,Precio,ConJuna,FechaPreparacion")] ProductoModel productoModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productoModel);
        }

        // GET: ProductoModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoModel productoModel = db.ProductoModel.Find(id);
            if (productoModel == null)
            {
                return HttpNotFound();
            }
            return View(productoModel);
        }

        // POST: ProductoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductoModel productoModel = db.ProductoModel.Find(id);
            db.ProductoModel.Remove(productoModel);
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
