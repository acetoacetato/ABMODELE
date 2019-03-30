using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace testSqlSeverMVC.Models
{
    public class testSqlSeverMVCContext : DbContext
    {
        public testSqlSeverMVCContext (DbContextOptions<testSqlSeverMVCContext> options)
            : base(options)
        {
        }

        public DbSet<testSqlSeverMVC.Models.Movie> Movie { get; set; }
    }
}
