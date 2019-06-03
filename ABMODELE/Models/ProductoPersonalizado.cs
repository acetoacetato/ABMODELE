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
        [Column(Order = 1)]
        public int idOrden { get; set; }

        [Key]
        [Column(Order = 2)]
        public int idProducto { get; set; }

        public virtual ProductoModel ProductoModel { get; set; }
        public virtual OrdenModel OrdenModel { get; set; }
        public virtual ICollection<ConSinIngrediente> ConSinIngrediente { get; set; }

    }
}