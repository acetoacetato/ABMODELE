using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ABMODELE.Models
{
    public class MyDbContext: DbContext
    {
        public DbSet<IngredienteModel> IngredienteModel { get; set; }
        public DbSet <ProductoModel> ProductoModel { get; set; }
        public DbSet<OrdenModel> OrdenModel { get; set; }
        public DbSet<UsuarioModel> UsuarioModel { get; set; }
        public DbSet<ProductoPersonalizado> ProductoPersonalizado { get; set; }
        
        public DbSet <ProductoToIngrediente> ProductoToIngrediente { get; set; }
        public DbSet <ConSinIngrediente> ConSinIngredientes { get; set; }
        
    }
}