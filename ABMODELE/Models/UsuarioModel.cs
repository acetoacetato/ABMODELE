using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABMODELE.Models
{
    public class UsuarioModel
    {
        [Key]
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public int Rol { get; set; }
        public int Saldo { get; set; }

        public virtual ICollection<OrdenModel> OrdenModel { get; set; }
    }
}