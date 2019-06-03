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
    public class IngredienteModelsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: IngredienteModels
        public ActionResult Index()
        {
            return View(db.IngredienteModel.ToList());
        }

        // GET: IngredienteModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredienteModel ingredienteModel = db.IngredienteModel.Find(id);
            if (ingredienteModel == null)
            {
                return HttpNotFound();
            }
            return View(ingredienteModel);
        }

        // GET: IngredienteModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IngredienteModels/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IngredienteId,Disponibilidad,Tipo,PrecioSingular")] IngredienteModel ingredienteModel)
        {
            if (ModelState.IsValid)
            {
                db.IngredienteModel.Add(ingredienteModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ingredienteModel);
        }

        // GET: IngredienteModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredienteModel ingredienteModel = db.IngredienteModel.Find(id);
            if (ingredienteModel == null)
            {
                return HttpNotFound();
            }
            return View(ingredienteModel);
        }

        // POST: IngredienteModels/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IngredienteId,Disponibilidad,Tipo,PrecioSingular")] IngredienteModel ingredienteModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredienteModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ingredienteModel);
        }

        // GET: IngredienteModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredienteModel ingredienteModel = db.IngredienteModel.Find(id);
            if (ingredienteModel == null)
            {
                return HttpNotFound();
            }
            return View(ingredienteModel);
        }

        // POST: IngredienteModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IngredienteModel ingredienteModel = db.IngredienteModel.Find(id);
            db.IngredienteModel.Remove(ingredienteModel);
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
