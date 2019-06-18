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
      

        public JsonResult AgregarProducto(ProductoPersonalizado productoPersonalizado)
        {
            CarroDeCompra _carro = (CarroDeCompra)HttpContext.Session["Carrito"];
            _carro.AgregarProducto(productoPersonalizado);
            return Json("'Sucess'");
        }

        public JsonResult EliminarProducto (ProductoPersonalizado productoPersonalizado)
        {
            CarroDeCompra _carro = (CarroDeCompra)HttpContext.Session["Carrito"];
            _carro.EliminarProducto(productoPersonalizado);
            return Json("'Sucess'");
        }

        public JsonResult VaciarCarrito()
        {
            CarroDeCompra _carro = (CarroDeCompra)HttpContext.Session["Carrito"];
            _carro.VaciarCarro();
            return Json("'Sucess'");
        }





    }
}