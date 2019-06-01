using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaMVC.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string nombreUsuario { get; set; }
        public string password { get; set; }
        public int rol { get; set; }
        public float saldo { get; set; }
    }
}