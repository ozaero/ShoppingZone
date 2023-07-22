using ShoppingPort.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ShoppingPort.Models.Context
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            var categoriler = new List<Category>()
            {
                new Category() {Name = "Kamera", Description = "Kamera ürünleri"},
                new Category() {Name = "Bilgisayar", Description = "Bilgisayar ürünleri"},
                new Category() {Name = "Elektro", Description = "Elektro ürünleri"},
                new Category() {Name = "Tablet", Description = "Tablet ürünleri"},
                new Category() {Name = "Temizlik", Description = "Temizlik ürünleri"}
            };

            foreach (var k in categoriler)
            {
                context.Categories.Add(k);
            }
            context.SaveChanges();

            var urunler = new List<Product>()
            {
                new Product() {Name = "Nikon", Description = "Nikon ürünü", Price = 56, Stock = 5, IsApprroved = true, IsHome = true,  CategoryId = 1},
                new Product() {Name = "Kodak", Description = "Kodak ürünü", Price = 56, Stock = 5, IsApprroved = true, IsHome = true,CategoryId = 2},
                new Product() {Name = "Hilti", Description = "Hilti ürünü", Price = 56, Stock = 5, IsApprroved = false, IsHome = true,CategoryId = 2},
                new Product() {Name = "Çiko", Description = "Çiko ürünü", Price = 56, Stock = 5, IsApprroved = true, IsHome = false,CategoryId = 3},
            };

            foreach (var p in urunler)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}