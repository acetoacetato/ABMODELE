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
    [Authorize(Roles = "administrador")]
    public class IngredientesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ingredientes
        public ActionResult Index()
        {
            return View(db.Ingrediente.Where(i => i.EsAuxiliar == false).ToList());
        }

        public JsonResult ingredientesDisponibles()
        {
            var listaIngredientes = db.Ingrediente
                                        .Where(i => i.EsAuxiliar == false)
                                        .Select(i => new { i.IngredienteId, i.Nombre })
                                        .ToList();
            JsonResult json = Json(listaIngredientes, JsonRequestBehavior.AllowGet);
            return json;
        }

 

        // GET: Ingredientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingrediente ingrediente = db.Ingrediente.Find(id);
            if (ingrediente == null || ingrediente.EsAuxiliar)
            {
                return HttpNotFound();
            }
            return View(ingrediente);
        }

        // GET: Ingredientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingredientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IngredienteId,Disponibilidad,Tipo,PrecioSingular,Nombre")] Ingrediente ingrediente)
        {
            if (ModelState.IsValid)
            {
                ingrediente.EsAuxiliar = false;
                db.Ingrediente.Add(ingrediente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ingrediente);
        }

        // GET: Ingredientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingrediente ingrediente = db.Ingrediente.Find(id);
            if (ingrediente == null || ingrediente.EsAuxiliar)
            {
                return HttpNotFound();
            }
            return View(ingrediente);
        }

        // POST: Ingredientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IngredienteId,Disponibilidad,Tipo,PrecioSingular,Nombre")] Ingrediente ingrediente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingrediente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ingrediente);
        }

        // GET: Ingredientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingrediente ingrediente = db.Ingrediente.Find(id);
            if (ingrediente == null || ingrediente.EsAuxiliar)
            {
                return HttpNotFound();
            }
            return View(ingrediente);
        }

        // POST: Ingredientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingrediente ingrediente = db.Ingrediente.Find(id);
            db.Ingrediente.Remove(ingrediente);
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
