﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ABMODELE.Models;
using ABMODELE.Models.ViewModel;

namespace ABMODELE.Controllers
{

    public class ProductosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Productoes
        public ActionResult Index()
        {
            
            return View(db.Producto.ToList());
        }

        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }


        // GET: Productoes/Create
        [Authorize(Roles = "administrador")]
        public ActionResult Create()
        {
            CreateProductoViewModel modelo = new CreateProductoViewModel();

            modelo.TipoProducto = db.TipoProducto.ToList();

            return View(modelo);
        }

        // POST: Productoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public ActionResult Create([Bind(Include = "ProductoId,Nombre,Precio,ConJuna,tiempoPreparacion,Tipo,Destacado")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                if (producto.Tipo != 1) //Si no es preparable
                {
                    producto.TiempoPreparacion = 0;
                    ProductoSingular(producto);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }

       


        public ActionResult IngredienteRow(int i, int idProd)
        {
            return PartialView("IngredientePartial", new ProductoToIngredienteViewModel() { index=i, productoToIngrediente = new ProductoToIngrediente() { IngredienteId = 0, CantidadProducto = 0, ProductoId = idProd} });
        }

        /// <summary>
        /// Agrega un ingrediente auxiliar y lo enlaza a un producto que no se prepara
        /// </summary>
        /// <param name="producto">El producto No Preparable a asignar</param>
        private void ProductoSingular(Producto producto)
        {
            Ingrediente Ingrediente = new Ingrediente
            {
                Nombre = producto.Nombre,
                Disponibilidad = 0,
                PrecioSingular = producto.Precio,
                EsAuxiliar = true
            };

            db.Ingrediente.Add(Ingrediente);
            db.SaveChanges();

            ProductoToIngrediente prodToIng = new ProductoToIngrediente
            {
                ProductoId = producto.ProductoId,
                IngredienteId = Ingrediente.IngredienteId,
                CantidadProducto = 1
            };

            db.ProductoToIngrediente.Add(prodToIng);
            db.SaveChanges();
        }



        // GET: Productoes/Edit/5
        [Authorize(Roles = "administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        /// <summary>
        ///  Calcula la cantidad de un producto que se pueden hacer.
        /// </summary>
        /// <param name="idProducto"> Id del producto a calcular</param>
        /// <returns> La cantidad de productos que se pueden fabricar con el inventario actual</returns>
        private int CantidadExistencias(int idProducto)
        {
            Producto producto = db.Producto.Find(idProducto);

            //Se obtiene la dupla de {Ingrediente.Disponibilidad, ProductoToIngrediente.CantidadProducto}
            //  de cada uno de los ingredientes que tiene un producto y se retorna en una lista.
            var  listaIngredientes = db.Ingrediente
                                        .Join(db.ProductoToIngrediente,     // La tabla con la que se va a unir
                                            ingrediente => ingrediente.IngredienteId,       // La primary key de la primera tabla
                                            productoToIngrediente => productoToIngrediente.IngredienteId,
                                            (ingrediente, productoToIngrediente) => 
                                                new { Ingrediente = ingrediente,ProductoToIngrediente = productoToIngrediente })        // Como se relacionan las variables
                                        .Where(join => join.ProductoToIngrediente.ProductoId == idProducto)     // La condicion para el select
                                        .Select( o => new { o.Ingrediente.Disponibilidad, o.ProductoToIngrediente.CantidadProducto})        // Se seleccionan sólo Disponibilidad y cantidad de producto
                                        .ToList();      // Se retorna una lista
            // Se obtiene la menor cantidad disponible
            var menor= listaIngredientes.First();
            foreach (var item in listaIngredientes)
            {
                var usoMenor = menor.Disponibilidad / menor.CantidadProducto;
                var usoItem = item.Disponibilidad / item.CantidadProducto;
                if (usoMenor > usoItem)
                    menor = item;
            }

            // Se retorna la cantidad de productos que se podrían hacer con esos ingredientes.
            return (int) (menor.Disponibilidad / menor.CantidadProducto);
        }

        // POST: Productoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public ActionResult Edit([Bind(Include = "ProductoId,Nombre,Precio,ConJuna,tiempoPreparacion,Tipo,Destacado,ProductoToIngredientes")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                //Ingredientes de la base de datos
                var listaAux = db.ProductoToIngrediente.Where(o => o.ProductoId == producto.ProductoId);

                var lista = (listaAux == null) ? new List<ProductoToIngrediente>(): listaAux.ToList();
                //Ingredientes del producto modificado
                var lista2 = (producto.ProductoToIngredientes == null)? new List<ProductoToIngrediente>():producto.ProductoToIngredientes.ToList();

                //Marca las cosas como agregadas
                foreach (var relacion in lista2)
                {
                    var itemAgregado = lista.Where(o => o.IngredienteId == relacion.IngredienteId);

                    //Si es un producto nuevo, se le marca como agregado
                    if (itemAgregado == null || itemAgregado.Count() == 0)
                    {
                        //Agregar relacion a la base de datos
                        //db.ProductoToIngrediente.Add(relacion);
                        db.Entry(relacion).State = EntityState.Added;
                    }
                    //Sino, se le quita de la lista para que no explote
                    else
                    {
                        producto.ProductoToIngredientes.Remove(relacion);
                    }
                }

                foreach(var relacion in lista)
                {
                    var itemAgregado = lista2.Where(o => o.IngredienteId == relacion.IngredienteId);

                    //Si es un producto nuevo, se le marca como agregado
                    if (itemAgregado == null || itemAgregado.Count() == 0)
                    {
                        //Agregar relacion a la base de datos
                        //db.ProductoToIngrediente.Add(relacion);
                        db.Entry(relacion).State = EntityState.Deleted;
                    }
                    //Sino, se le quita de la lista para que no explote
                    else
                    {
                        producto.ProductoToIngredientes.Remove(relacion);
                    }
                }

                //db.SaveChanges();
                //ModelState.Clear();
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Productoes/Delete/5
        [Authorize(Roles = "administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            Ingrediente ingrediente = producto.ProductoToIngredientes
                                                .First()
                                                .Ingrediente;
            db.Producto.Remove(producto);
            if (producto.TipoProducto.Nombre != "Preparable" )
                db.Ingrediente.Remove(ingrediente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult Browse(string categorias)
        {
            int idCat = db.Categoria
                            .Where(cat => cat.nombre == categorias)
                            .Select(cat => cat.id)
                            .FirstOrDefault();

            var listaProductos = db.Producto
                                    .Join(db.CategoriaToProducto,
                                    producto => producto.ProductoId,
                                    categoriaToProducto => categoriaToProducto.ProductoId,
                                    (producto, categoriaToProducto) =>
                                            new { Producto = producto, CategoriaToProducto = categoriaToProducto })
                                    .Where(join => join.CategoriaToProducto.CategoriaId == idCat)
                                    .Select(join => join.Producto)
                                    .ToList();

            return View(listaProductos);
        }




        /// <summary>
        /// Busca en el nombre de los productos, los términos a buscar.
        /// </summary>
        /// <param name="str">los términos a buscar, separados por espacio.</param>
        /// <returns>Un Json con los productos coincidentes.</returns>
        [HttpPost]
        public JsonResult busqueda(string str)
        {
            String[] terminos = str.Split(' ');
            List<Producto> lista = new List<Producto>();
            foreach(var termino in terminos)
            {
                var auxLista = db.Producto.Where(o => o.Nombre.Contains(termino));
                lista.AddRange(auxLista);
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

    }
}
