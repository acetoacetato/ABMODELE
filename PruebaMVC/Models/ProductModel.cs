using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaMVC.Models
{
    public class ProductModel
    {
        private List<Product> products;

        public ProductModel()
        {
            this.products = new List<Product>() {
                new Product {
                    Id = "p01",
                    Name = "Name 1",
                    Price = 5,
                    Photo = "thumb1.gif"
                },
                new Product {
                    Id = "p02",
                    Name = "Name 2",
                    Price = 2,
                    Photo = "thumb2.gif"
                },
                new Product {
                    Id = "p03",
                    Name = "Name 3",
                    Price = 6,
                    Photo = "thumb3.gif"
                }
            };
        }

        public List<Product> findAll()
        {
            return this.products;
        }

        public Product find(string id)
        {
            return this.products.Single(p => p.Id.Equals(id));
        }
    }
}