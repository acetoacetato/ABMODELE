using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABMODELE.Models
{
    public class CarroDeCompra
    {
        public List<ProductoPersonalizado> ProductoPersonalizado { get; set; }

        internal void AgregarProducto(ProductoPersonalizado productoPersonalizado)
        {
            ProductoPersonalizado.Add(productoPersonalizado);
        }

        internal void EliminarProducto(ProductoPersonalizado productoPersonalizado)
        {
            ProductoPersonalizado.Remove(productoPersonalizado);
        }

        internal void VaciarCarro()
        {
            foreach(var item in ProductoPersonalizado)
            {
                ProductoPersonalizado.Remove(item);
            }
        }

        public int CalcularCoste()
        {
            int total = 0;
            foreach(var item in ProductoPersonalizado)
            {
                total += item.calcularCoste();
            }
            return total;
        }
    }
}