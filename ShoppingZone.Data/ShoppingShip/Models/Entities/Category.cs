using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingPort.Models.Entities
{
    public class Category
    {
        public List<Product> ProductList { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}