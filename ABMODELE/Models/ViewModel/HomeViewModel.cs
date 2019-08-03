using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABMODELE.Models.ViewModel
{
    public class HomeViewModel
    {

        public List <Categoria> categorias { get; set; }
        public List <Producto> productos { get; set; }



    }
}