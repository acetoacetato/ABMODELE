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
        [Key, ForeignKey("Producto")]
        [Column(Order = 1)]
        public int ProductoId { get; set; }

        [Key, ForeignKey("Ingrediente")]
        [Column(Order = 2)]
        public int IngredienteId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "No puede ser un valor menor a 0")]
        public float CantidadProducto { get; set; }

        public virtual Producto Producto { get; set; }
        public virtual Ingrediente Ingrediente { get; set; }
    }
}