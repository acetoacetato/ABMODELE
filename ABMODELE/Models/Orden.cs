using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ABMODELE.Models
{
    public class Orden
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NumOrden { get; set; }
        public string IdUsuario { get; set; }
        public DateTime FechaOrden { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int Monto { get; set; }
        public bool Pagado { get; set; }
        public string Comentario { get; set; }
        [ForeignKey("MetodoDePago")]
        public int MetodoPago { get; set; }
        public bool Preparado { get; set; }
        public bool Entregado { get; set; }


        public virtual ICollection<ProductoPersonalizado> ProductoPersonalizado { get; set; }
        public virtual MetodoDePago MetodoDePago { get; set; }




    }

   public class OrdenJson
    {
        public int NumOrden { get; set; }
        public string FechaEntrega { get; set; }
        public ICollection<ProductoPersonalizado> ProductoPersonalizado { get; set; }
        public OrdenJson (Orden o)
        {
            NumOrden = o.NumOrden;
            FechaEntrega = o.FechaEntrega.ToString();
            ProductoPersonalizado = o.ProductoPersonalizado;
        }
    }


}