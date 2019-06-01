using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaMVC.Models
{
    public class Product
    {
        public string Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public double Price
        {
            get;
            set;
        }

        public string Photo
        {
            get;
            set;
        }

        public bool ConJuna { get; set; }

        //tiempo en minutos
        public int TiempoPreparacion {get; set;}

        public virtual ICollection <Ingrediente> Ingredientes { get; set; }

    }
}
