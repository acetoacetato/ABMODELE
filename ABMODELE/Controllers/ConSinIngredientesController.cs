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
    public class ConSinIngredientesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: ConSinIngredientes
        public ActionResult Index()
        {
            return View(db.ConSinIngredientes.ToList());
        }

        // GET: ConSinIngredientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConSinIngrediente conSinIngrediente = db.ConSinIngredientes.Find(id);
            if (conSinIngrediente == null)
            {
                return HttpNotFound();
            }
            return View(conSinIngrediente);
        }

        // GET: ConSinIngredientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConSinIngredientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProductoPersonalizado,IdIngredienteModel")] ConSinIngrediente conSinIngrediente)
        {
            if (ModelState.IsValid)
            {
                db.ConSinIngredientes.Add(conSinIngrediente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(conSinIngrediente);
        }

        // GET: ConSinIngredientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConSinIngrediente conSinIngrediente = db.ConSinIngredientes.Find(id);
            if (conSinIngrediente == null)
            {
                return HttpNotFound();
            }
            return View(conSinIngrediente);
        }

        // POST: ConSinIngredientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProductoPersonalizado,IdIngredienteModel")] ConSinIngrediente conSinIngrediente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conSinIngrediente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(conSinIngrediente);
        }

        // GET: ConSinIngredientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConSinIngrediente conSinIngrediente = db.ConSinIngredientes.Find(id);
            if (conSinIngrediente == null)
            {
                return HttpNotFound();
            }
            return View(conSinIngrediente);
        }

        // POST: ConSinIngredientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConSinIngrediente conSinIngrediente = db.ConSinIngredientes.Find(id);
            db.ConSinIngredientes.Remove(conSinIngrediente);
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
