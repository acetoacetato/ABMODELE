using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ABMODELE.Models
{
    public class ProductoToIngrediente
    {
        [Key, ForeignKey("ProductoModel")]
        [Column(Order=1)]
        public int ProductoId { get; set; }

        [Key, ForeignKey("IngredienteModel")]
        [Column(Order=2)]
        public int IngredienteId { get; set; }

        public float CantidadProducto { get; set; }

        public virtual ProductoModel ProductoModel { get; set; }
        public virtual IngredienteModel IngredienteModel { get; set; }
    }
}