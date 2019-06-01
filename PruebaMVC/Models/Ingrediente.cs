using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaMVC.Models
{
    public class Ingrediente
    {
        public int id { get; set; }

        public float disponibilidad { get; set; }
        public string tipo { get; set; }
        public int precioSingular { get; set; }

        public virtual ICollection<Product> Productos { get; set; }
    }
}