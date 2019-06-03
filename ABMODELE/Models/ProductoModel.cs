using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABMODELE.Models
{
    public class ProductoModel
    {
        [Key]
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public bool ConJuna { get; set; }
        public DateTime FechaPreparacion { get; set; }

        public virtual ICollection<ProductoToIngrediente> ProductoToIngredientes { get; set; }
        public virtual ICollection<ProductoPersonalizado> ProductoPersonalizados { get; set; }

    }
}