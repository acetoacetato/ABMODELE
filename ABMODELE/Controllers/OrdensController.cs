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
using System.Web.Helpers;
using ABMODELE.Models.ViewModel;

namespace ABMODELE.Controllers
{
    //Sólo los roles de administrador y cocineros pueden entrar a estas vistas
    /// <summary>
    /// Controlador de las ordenes
    /// </summary>
    public class OrdensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private CarroDeCompra obtenerCarro()
        {
            CarroDeCompra carrito = (CarroDeCompra)HttpContext.Session["Carrito"];
            if(carrito == null)
            {
                carrito = new CarroDeCompra();
                HttpContext.Session["Carrito"] = carrito;
            }

            return carrito;
        }

        /// <summary>
        /// Lista las ordenes existentes, sólo se autoriza a los administradores y a los cocineros
        /// </summary>
        /// <param name="todas">inicializado en un valor si se deben mostrar todas las ordenes</param>
        /// <returns></returns>
        [Authorize(Roles = "administrador, cocinero")]
        public async Task<ActionResult> Index(bool? todas)
        {
            List<Orden> ordenes;
            // Si se especifica el parámetro todas en true,
            //      se muestran todas las ordenes,  
            //      ordenadas por fecha de entrega.
            if(todas != null && todas == true)
            {
                //Todas las órdenes, desde principios de los tiempos
                ordenes = await db.Orden.OrderBy(o => o.FechaEntrega).ToListAsync();
            }
            //Sino, se muestran las órdenes de hoy que no están entregadas pero si pagadas
            else
            {
                var hoy = DateTime.Now;
                //Las órdenes que son para hoy y que no están entregadas, ordenadas por fecha
                ordenes = await db.Orden // La tabla Orden de la base de datos
                                    .Where(o => o.Entregado == false) // ordenes que no estén entregadas
                                    .Where(o => DbFunctions.DiffDays(o.FechaEntrega, hoy) == 0) // Ordenes que tengan como fecha de entrega el día de hoy
                                    .Where(o => o.Pagado == true) // Ordenes que estén pagadas
                                    .OrderBy(o => o.FechaEntrega) // Ordenar las ordenes seleccionadas por la fecha de entrega
                                                                  //  (La fecha de entrega incluye hora)
                                    .ToListAsync(); //Se transforma la consulta a una lista

 
            }
            return View(ordenes); //Se retorna la vista con el modelo seleccionado
        }

        /// <summary>
        /// Retornan los datos de una orden.
        /// </summary>
        /// <param name="idOrden">El identificador de la orden a obtener.</param>
        /// <returns>Un JSON con los datos de una orden.</returns>
        public JsonResult obtenerOrden(int idOrden)
        {
            var orden = db.Orden.Find(idOrden);
            //Se crea un objeto OrdenJson que contiene sólo los datos necesarios,
            //  y se le transforma en un JSON para retornarlo.
            JsonResult json = Json(new OrdenJson(orden), JsonRequestBehavior.AllowGet);
                
            return json;

        }

        /// <summary>
        /// Genera la fecha aproximada de entrega de una orden
        /// </summary>
        /// <param name="idOrden">Id de la orden a generar</param>
        /// <returns>La fecha aproximada en la que se debería hacer el retido de la orden.</returns>
        private DateTime FechaEntrega(int idOrden)
        {
            // Se obtiene la orden que coincida con la id
            var orden = db.Orden.Find(idOrden);
            var fechaEntrega = orden.FechaOrden; // Fecha a la que quiero que salga
            var listaProductos = orden.ProductoPersonalizado; //La lista con los productos que contiene la orden
            int minutos=0;

            //Se recorre cada producto de la orden
            foreach(var item in listaProductos)
            {
                minutos += item.Producto.TiempoPreparacion; //Se le suma el tiempo de preparación de cada uno
            }
            orden.FechaEntrega = fechaEntrega.AddMinutes(minutos); //Se le suma el total de tiempo de preparación de cada producto de la orden
            db.SaveChanges(); //Se guardan los cambios en la base de datos
            return orden.FechaEntrega; //Se retorna la fecha aproximada de la entrega
        }

        /// <summary>
        /// Elimina una orden del sistema
        /// </summary>
        /// <param name="NumOrden"></param>
        /// <returns></returns>
        [Authorize(Roles = "administrador")]
        public ActionResult RechazarOrden(int NumOrden)
        {
            // Se obtiene la orden que coincida con la id
            var orden = db.Orden.Find(NumOrden);
            //Si la orden existe, se la elimina
            if(orden != null)
                db.Orden.Remove(orden);
            db.SaveChanges();
            //Se retorna a la vista 'Index' de Ordens
            return RedirectToAction("Index");
        }

        // GET: Ordens/Details/5
        [Authorize(Roles = "administrador, cocinero")]
        public async Task<ActionResult> Details(int? id)
        {
            //Si no se ingresa un id, se lanza una página de error
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Se busca la orden con ese id
            Orden orden = await db.Orden.FindAsync(id);
            //Si no se encuentra, se tira 404
            if (orden == null)
            {
                return HttpNotFound();
            }
            return View(orden);
        }

        // GET: Ordens/PagarCarro
        [Authorize]
        [ActionName("PagarCarro")]
        public ActionResult Create()           
        {
            //Si el usuario es un usuario o un administrador
            if (User.IsInRole("cliente") || User.IsInRole("administrador"))
            {
                //Se obtiene una instancia de carro que existe en las variables de sesion
                CarroDeCompra _carro = obtenerCarro();

                //Se obtiene el nombre de usuario (mail)
                string nombreUsr = User.Identity.Name;
                //Se crea una orden con los datos del carro de compras
                Orden orden = new Orden()
                {
                    IdUsuario = nombreUsr,
                    Monto = _carro.CalcularCoste()
                };
                CarroViewModel carroViewModel = new CarroViewModel()
                {
                    orden = orden,
                    productoPersonalizado = _carro.ProductoPersonalizado
                };

                return View(carroViewModel);


            }
            
            return RedirectToAction("Index");

        }
        /// <summary>
        /// Marca una orden como entregada.
        /// </summary>
        /// <param name="id">El identificador de la orden.</param>
        /// <returns>Un JSON con el resultado de la operación.</returns>
        [Authorize(Roles = "administrador, cocinero")]
        [HttpPost]
        public JsonResult EntregarOrden(int id)
        {
         
            Orden orden = db.Orden.Where(o => o.NumOrden == id).First<Orden>();
            orden.Entregado = true;
            db.SaveChanges();

            return Json("'Success':'true'");
            
        }

        // POST: Ordens/Create
        /// <summary>
        /// Crea una orden.
        /// </summary>
        /// <param name="orden">La orden a agregar a la base de datos.</param>
        /// <returns>La vista de la orden a crear.</returns>
        [Authorize(Roles = "administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NumOrden,IdUsuario,FechaOrden,FechaEntrega,Monto,Pagado,MetodoPago,Preparado,Entregado")] Orden orden)
        {
            // Insertamos la orden a la db 
            if (ModelState.IsValid)
            {
                db.Orden.Add(orden);
                await db.SaveChangesAsync();
            }
            // Agregamos los productos a la DB

            AgregarProductosPersonalizados(orden);

            

            return View(orden);
        }

        private void AgregarProductosPersonalizados(Orden orden)
        {

            var listaProductos = orden.ProductoPersonalizado;
            foreach(var item in listaProductos)
            {
                db.ProductoPersonalizado.Add(item);
            }
        }

        /// <summary>
        /// Abre la vista para editar
        /// </summary>
        /// <param name="id">Identificador de la orden a editar.</param>
        /// <returns>La vista de la orden editada.</returns>
        [Authorize(Roles = "administrador")]
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
        /// <summary>
        /// Aplica la edición de una orden a la base de datos
        /// </summary>
        /// <param name="orden">La orden con los datos editados.</param>
        /// <returns>La vista de edición con la orden editada.</returns>
        [Authorize(Roles = "administrador, cocinero")]
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

        /// <summary>
        /// Abre una vista para eliminar una orden.
        /// </summary>
        /// <param name="id">Identificador de la orden a eliminar.</param>
        /// <returns>La vista de eliminación con los datos de la orden especificada.</returns>
        [Authorize(Roles = "administrador, cocinero")]
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
        /// <summary>
        /// Elimina una orden de la base de datos.
        /// </summary>
        /// <param name="id">Identificador de la orden.</param>
        /// <returns>La vista mostrando todas las ordenes.</returns>
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "administrador, cocinero")]
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
