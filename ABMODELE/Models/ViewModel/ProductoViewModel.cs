using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABMODELE.Models.ViewModel
{
    public class CreateProductoViewModel
    {
        public Producto Producto { get; set; }
        public List<TipoProducto> TipoProducto { get; set; }

        public IEnumerable<SelectListItem> TiposProducto
        {
            get { return new SelectList(TipoProducto, "Id", "Nombre"); }
        }

    }
}