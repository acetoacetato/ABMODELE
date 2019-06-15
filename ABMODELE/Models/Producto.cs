using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ABMODELE.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public bool ConJuna { get; set; }
        [ForeignKey("TipoProducto")]
        public int Tipo { get; set; }
        [Display(Name ="Tiempo de preparacion")]
        public int TiempoPreparacion { get; set; }
        public virtual ICollection<ProductoToIngrediente> ProductoToIngredientes { get; set; }
        public virtual ICollection<ProductoPersonalizado> ProductoPersonalizados { get; set; }
        public virtual TipoProducto TipoProducto { get; set; }
    }
}