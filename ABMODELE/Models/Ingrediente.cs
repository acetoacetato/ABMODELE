using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABMODELE.Models
{
    public class Ingrediente
    {
        [Key]
        public int IngredienteId { get; set; }
        public string Nombre { get; set; }
        public float Disponibilidad { get; set; }
        public string Tipo { get; set; }
        public int PrecioSingular { get; set; }

        public virtual ICollection<ProductoToIngrediente> ProductoToIngrediente { get; set; }
        public virtual ICollection<SinIngrediente> SinIngrediente { get; set; }
    }
}