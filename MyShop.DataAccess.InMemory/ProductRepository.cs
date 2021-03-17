using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {

        

        ObjectCache cache = MemoryCache.Default;
        List<Product> products;


        public ProductRepository()
        {
            products = cache["products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
            }
        }


        //- save/commit :
        public void commit() {
            cache["products"] = products;
        }

        //-Insert :

        public void Insert(Product product)
        {
            products.Add(product);
        }


        //-Update  :
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else {
                throw new Exception("Product not found");
            }
        }

        //-Find  :
        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }


        //- return a List that can be queried :
        public IQueryable<Product> Collection() {
            return products.AsQueryable();
        }


        //-Delete  :
        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
