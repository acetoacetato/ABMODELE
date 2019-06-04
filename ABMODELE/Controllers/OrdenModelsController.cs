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
    public class OrdenModelsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: OrdenModels
        public ActionResult Index()
        {
            return View(db.OrdenModel.ToList());
        }

        // GET: OrdenModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenModel ordenModel = db.OrdenModel.Find(id);
            if (ordenModel == null)
            {
                return HttpNotFound();
            }
            return View(ordenModel);
        }

        // GET: OrdenModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrdenModels/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NumOrden,IdUsuario,FechaOrden,FechaEntrega,Monto,Pagado,MetodoPago,ListoEntrega,Entregado")] OrdenModel ordenModel)
        {
            if (ModelState.IsValid)
            {
                db.OrdenModel.Add(ordenModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ordenModel);
        }

        // GET: OrdenModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenModel ordenModel = db.OrdenModel.Find(id);
            if (ordenModel == null)
            {
                return HttpNotFound();
            }
            return View(ordenModel);
        }

        // POST: OrdenModels/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NumOrden,IdUsuario,FechaOrden,FechaEntrega,Monto,Pagado,MetodoPago,ListoEntrega,Entregado")] OrdenModel ordenModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordenModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ordenModel);
        }

        // GET: OrdenModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenModel ordenModel = db.OrdenModel.Find(id);
            if (ordenModel == null)
            {
                return HttpNotFound();
            }
            return View(ordenModel);
        }

        // POST: OrdenModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrdenModel ordenModel = db.OrdenModel.Find(id);
            db.OrdenModel.Remove(ordenModel);
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
