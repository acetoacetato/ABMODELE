using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ABMODELE.Models;
using System.Data.Entity.Core.Objects;

namespace ABMODELE.Controllers
{
    //Sólo los roles de administrador y cocineros pueden entrar a estas vistas
    [Authorize(Roles = "administrador, cocinero")]
    public class OrdensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ordens
        public async Task<ActionResult> Index(bool? todas)
        {
            List<Orden> ordenes;
            if(todas != null)
            {
                //Todas las órdenes, desde principios de los tiempos
                ordenes = await db.Orden.ToListAsync();
            }
            else
            {
                var hoy = DateTime.Now;
                //Las órdenes que son para hoy y que no están entregadas, ordenadas por fecha
                ordenes = await db.Orden
                                    .Where(o => o.Entregado == false)
                                    .Where(o => DbFunctions.DiffDays(o.FechaEntrega, hoy) == 0)
                                    .OrderBy(o => o.FechaEntrega)
                                    .ToListAsync();

 
            }
            return View(ordenes);
        }


        // GET: Ordens/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = await db.Orden.FindAsync(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            return View(orden);
        }

        // GET: Ordens/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EntregarOrden(int id)
        {
         
            Orden orden = db.Orden.Where(o => o.NumOrden == id).First<Orden>();
            orden.Entregado = true;
            db.SaveChanges();

            return Json("'Success':'true'");
            
        }

        // POST: Ordens/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NumOrden,IdUsuario,FechaOrden,FechaEntrega,Monto,Pagado,MetodoPago,Preparado,Entregado")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                db.Orden.Add(orden);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(orden);
        }

        // GET: Ordens/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = await db.Orden.FindAsync(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            return View(orden);
        }

        // POST: Ordens/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "NumOrden,IdUsuario,FechaOrden,FechaEntrega,Monto,Pagado,MetodoPago,Preparado,Entregado")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orden).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(orden);
        }

        // GET: Ordens/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orden orden = await db.Orden.FindAsync(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            return View(orden);
        }

        // POST: Ordens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Orden orden = await db.Orden.FindAsync(id);
            db.Orden.Remove(orden);
            await db.SaveChangesAsync();
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
