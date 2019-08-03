using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ABMODELE.Models
{
    public class CategoriaToProducto
    {

        [Key, ForeignKey("Categoria")]
        [Column(Order = 1)]
        public int CategoriaId { get; set; }

        [Key, ForeignKey("Producto")]
        [Column(Order = 2)]
        public int ProductoId { get; set; }


        public virtual Categoria Categoria { get; set; }
        public virtual Producto Producto { get; set; }
    }
}