using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABMODELE.Models.ViewModel
{
    public class CarroViewModel
    {
        public Orden orden { get; set; }
        private List<ProductoPersonalizado> _productoPersonalizado { get; set; }
        public List<ProductoPersonalizado> productoPersonalizado
                                            {
                                                get
                                                {
                                                    if (this._productoPersonalizado == null)
                                                        this._productoPersonalizado = new List<ProductoPersonalizado>();
                                                    return this._productoPersonalizado ;
                                                }
                                                set
                                                {
                                                    this._productoPersonalizado = value;
                                                }
                                            }

        public List<MetodoDePago> MetodoDePago { get; set; }
        public IEnumerable<SelectListItem> MetodosDePago
        {
            get { return new SelectList(MetodoDePago, "Id", "Nombre"); }
        }


        public CarroViewModel()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MetodoDePago = db.MetodoDePago.ToList();
        }
    }
}