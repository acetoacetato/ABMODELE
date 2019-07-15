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

        public JsonResult mostrarCarrito()
        {
            CarroDeCompra _carro = obtenerCarro();
            var carro = Json(_carro, JsonRequestBehavior.AllowGet);
            return carro;
        }

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

        
        

        public JsonResult EliminarProducto (ProductoPersonalizado productoPersonalizado)
        {
            CarroDeCompra _carro = obtenerCarro();
            _carro.EliminarProducto(productoPersonalizado);
            return Json("'Sucess'");
        }

        public JsonResult VaciarCarrito()
        {
            CarroDeCompra _carro = obtenerCarro();
            _carro.VaciarCarro();
            return Json("'Sucess'");
        }





    }
}