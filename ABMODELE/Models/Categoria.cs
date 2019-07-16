using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ABMODELE.Models
{
    public class Categoria
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public String nombre { get; set; }

        public virtual ICollection<CategoriaToProducto> CategoriaToProducto { get; set; }

    }
}