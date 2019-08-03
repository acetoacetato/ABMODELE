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
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un precio mayor o igual a 0")]
        public int Precio { get; set; }
        [Display(Name = "¿Comprable Con junaeb?")]
        public bool ConJuna { get; set; }
        [ForeignKey("TipoProducto")]
        [Display(Name = "Tipo de Producto")]
        public int Tipo { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un tiempo mayor o igual a 0")]
        [Display(Name ="Tiempo de preparacion (En minutos)")]
        public int TiempoPreparacion { get; set; }
        [Display (Name = "Producto Destacado")]
        public bool Destacado { get; set; }

        public virtual ICollection<ProductoToIngrediente> ProductoToIngredientes { get; set; }
        public virtual ICollection<ProductoPersonalizado> ProductoPersonalizados { get; set; }
        public virtual ICollection<CategoriaToProducto> CategoriaToProducto { get; set; }
        public virtual TipoProducto TipoProducto { get; set; }
    }
}