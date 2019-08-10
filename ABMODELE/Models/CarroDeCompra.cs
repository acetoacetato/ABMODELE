using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABMODELE.Models
{
    public class CarroDeCompra
    {
        public List<ProductoPersonalizado> ProductoPersonalizado { get; set; }

        public CarroDeCompra()
        {
            ProductoPersonalizado = new List<ProductoPersonalizado>();
        }

        internal void AgregarProducto(ProductoPersonalizado productoPersonalizado)
        {
            if (ProductoPersonalizado == null)
                return;
            ProductoPersonalizado.Add(productoPersonalizado);
        }

        internal void EliminarProducto(ProductoPersonalizado productoPersonalizado)
        {
            if (ProductoPersonalizado == null)
                return;
            ProductoPersonalizado.Remove(productoPersonalizado);
        }

        internal void VaciarCarro()
        {
            if (ProductoPersonalizado == null)
                return;
            ProductoPersonalizado.Clear();

        }

        public int CalcularCoste()
        {
            if (ProductoPersonalizado == null)
                return 0;
            int total = 0;
            foreach(var item in ProductoPersonalizado)
            {
                total += item.calcularCoste();
            }
            return total;
        }
    }
}