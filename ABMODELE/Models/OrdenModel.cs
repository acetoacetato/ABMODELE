using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABMODELE.Models
{
    public class OrdenModel
    {
        [Key]
        public int NumOrden { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaOrden { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int Monto { get; set; }
        public bool Pagado { get; set; }
        public int MetodoPago { get; set; }
        public bool ListoEntrega { get; set; }
        public bool Entregado { get; set; }


        public virtual ICollection<ProductoPersonalizado> ProductoPersonalizado { get; set; }
    }
}