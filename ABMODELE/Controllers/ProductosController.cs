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

    public class ProductosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Productoes
        public ActionResult Index()
        {
            
            return View(db.Producto.ToList());
        }

        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }


        // GET: Productoes/Create
        [Authorize(Roles = "administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public ActionResult Create([Bind(Include = "ProductoId,Nombre,Precio,ConJuna")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        // GET: Productoes/Edit/5
        [Authorize(Roles = "administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        /// <summary>
        ///  Calcula la cantidad de un producto que se pueden hacer.
        /// </summary>
        /// <param name="idProducto"> Id del producto a calcular</param>
        /// <returns> La cantidad de productos que se pueden fabricar con el inventario actual</returns>
        private int CantidadExistencias(int idProducto)
        {
            Producto producto = db.Producto.Find(idProducto);

            //Se obtiene la dupla de {Ingrediente.Disponibilidad, ProductoToIngrediente.CantidadProducto}
            //  de cada uno de los ingredientes que tiene un producto y se retorna en una lista.
            var  listaIngredientes = db.Ingrediente
                                        .Join(db.ProductoToIngrediente,     // La tabla con la que se va a unir
                                            ingrediente => ingrediente.IngredienteId,       // La primary key de la primera tabla
                                            productoToIngrediente => productoToIngrediente.IngredienteId,
                                            (ingrediente, productoToIngrediente) => 
                                                new { Ingrediente = ingrediente,ProductoToIngrediente = productoToIngrediente })        // Como se relacionan las variables
                                        .Where(join => join.ProductoToIngrediente.ProductoId == idProducto)     // La condicion para el select
                                        .Select( o => new { o.Ingrediente.Disponibilidad, o.ProductoToIngrediente.CantidadProducto})        // Se seleccionan sólo Disponibilidad y cantidad de producto
                                        .ToList();      // Se retorna una lista

            // Se obtiene la menor cantidad disponible
            var menor= listaIngredientes.First();
            foreach (var item in listaIngredientes)
            {
                var usoMenor = menor.Disponibilidad / menor.CantidadProducto;
                var usoItem = item.Disponibilidad / menor.CantidadProducto;
                if (usoMenor > usoItem)
                    menor = item;
            }

            // Se retorna la cantidad de productos que se podrían hacer con esos ingredientes.
            return (int) (menor.Disponibilidad / menor.CantidadProducto);
        }

        // POST: Productoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public ActionResult Edit([Bind(Include = "ProductoId,Nombre,Precio,ConJuna")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Productoes/Delete/5
        [Authorize(Roles = "administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
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
