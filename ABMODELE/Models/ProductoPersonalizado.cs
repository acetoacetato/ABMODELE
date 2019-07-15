using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ABMODELE.Models
{
    public class ProductoPersonalizado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(Order = 1)]
        public int IdOrden { get; set; }

        [Column(Order = 2)]
        public int IdProducto { get; set; }


        public virtual Producto Producto { get; set; }
        public virtual Orden Orden { get; set; }
        public virtual ICollection<SinIngrediente> SinIngrediente { get; set; }
       


        public int calcularCoste()
        {
            if(SinIngrediente == null)
            {
                return 0;
            }
            int total = Producto.Precio;
            var IngExtra = SinIngrediente
                            .Where(s => s.Sin == false)
                            .Select(s => s.Ingrediente)
                            .ToList();
            foreach(var item in IngExtra)
            {
                total += item.PrecioSingular;
            }
            return total;
        }
    }
}