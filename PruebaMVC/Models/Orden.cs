using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaMVC.Models
{
    public class Orden
    {
        public int id_usuario { get; set; }

        public int numeroOrden { get; set; }

        public DateTime fechaPreparacion { get; set; }

        public DateTime fechaEntrega { get; set; }

        public float monto { get; set; }

        public Boolean pagado { get; set; }

        public int metodoPago { get; set; }

        public Boolean listoEntrega { get; set; }

        public Boolean entregado { get; set; }
    }
}