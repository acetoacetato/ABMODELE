using ABMODELE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABMODELE.Controllers
{
    public class CarritoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Obtiene el carro de la sesión. Si no existe, se crea uno.
        /// </summary>
        /// <returns>el carro de la sesión</returns>
        private CarroDeCompra obtenerCarro()
        {
            CarroDeCompra carrito = (CarroDeCompra)HttpContext.Session["Carrito"];
            if (carrito == null)
            {
                carrito = new CarroDeCompra();
                HttpContext.Session["Carrito"] = carrito;
            }

            return carrito;
        }

        /// <summary>
        /// Muestra los datos del carro.
        /// </summary>
        /// <returns>Un Json con los datos del carro.</returns>
        public JsonResult mostrarCarrito()
        {
            CarroDeCompra _carro = obtenerCarro();
            var carro = Json(_carro, JsonRequestBehavior.AllowGet);
            return carro;
        }

        /// <summary>
        /// Agrega una cantidad de productos al carro.
        /// </summary>
        /// <param name="idProducto">El id del producto a agregar.</param>
        /// <param name="cantidad">La cantidad de productos a agregar.</param>
        /// <returns>Un Json con el resultado de la operación.</returns>
        [HttpPost]
        public JsonResult AgregarProducto(int idProducto, int cantidad)
        {
            CarroDeCompra _carro = obtenerCarro();
            Producto producto = db.Producto.Find(idProducto);
            if(producto == null)
            {
                return Json(new { success = false, responseText = "El id del rpoducto es inválido." }, JsonRequestBehavior.AllowGet);
            }
            
            if (_carro == null)
            {
                _carro = new CarroDeCompra();
                HttpContext.Session["Carrito"] = _carro;
            }

            for(int i=0; i< cantidad; i++)
            {
                ProductoPersonalizado productoPersonalizado =
                new ProductoPersonalizado()
                {
                    IdProducto = idProducto,
                    Producto = producto,
                };
                _carro.AgregarProducto(productoPersonalizado);
            }
            
            return Json(new {success = true}, JsonRequestBehavior.AllowGet);
        }

        
        
        /// <summary>
        /// Elimina un producto del carro.
        /// </summary>
        /// <param name="productoPersonalizado">El producto a eliminar</param>
        /// <returns>Un Json con el resultado de la operación.</returns>
        public JsonResult EliminarProducto (ProductoPersonalizado productoPersonalizado)
        {
            CarroDeCompra _carro = obtenerCarro();
            _carro.EliminarProducto(productoPersonalizado);
            return Json("'Sucess'");
        }

        /// <summary>
        /// Elimina todos los productos del carro.
        /// </summary>
        /// <returns>Un Json con el resultado de la operación.</returns>
        public JsonResult VaciarCarrito()
        {
            CarroDeCompra _carro = obtenerCarro();
            _carro.VaciarCarro();
            return Json("'Sucess'");
        }





    }
}