using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ABMODELE.Models
{
    public class ConSinIngrediente
    {
        [Key]
        [Column(Order=1)]
        public int IdProductoPersonalizado { get; set; }

        [Key]
        [Column(Order = 2)]
        public int IdIngredienteModel { get; set; }

        public virtual ProductoPersonalizado ProductoPersonalizado { get; set; }
        public virtual IngredienteModel IngredienteModel { get; set; }
    }
}