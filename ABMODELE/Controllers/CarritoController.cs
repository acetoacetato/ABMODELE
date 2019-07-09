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

        public JsonResult AgregarProducto(ProductoPersonalizado productoPersonalizado)
        {
            CarroDeCompra _carro = obtenerCarro();
            _carro.AgregarProducto(productoPersonalizado);
            return Json("'Sucess'");
        }

        [HttpPost]
        //recibir el id del producto y verificar si existe en la db
        public JsonResult AgregarProducto()
        {
            
            CarroDeCompra _carro = obtenerCarro();
            if(_carro == null)
            {
                _carro = new CarroDeCompra();
                HttpContext.Session["Carrito"] = _carro;
            }
            _carro.AgregarProducto(new ProductoPersonalizado() { IdProducto = 1, Producto = new Producto() });
            return Json("'Sucess'");
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