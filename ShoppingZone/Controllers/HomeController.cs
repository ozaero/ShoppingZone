
using ShoppingZone.Business.Repository;
using ShoppingZone.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingZone.Controllers
{
    public class HomeController : Controller
    {
        GenericRepository<Product> repositoryP = new GenericRepository<Product>();
        GenericRepository<Category> repositoryC = new GenericRepository<Category>();

        public ActionResult Home()
        {
            var item = repositoryP.GetAll();

            var newItem = item.Select(i => new ProductView()
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description.Length > 50 ? i.Description.Substring(0, 50) + "..." : i.Description,
                Price = i.Price,
                Image = i.Image
                

            }).ToList();

            return View(newItem);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult ProductMenu(int? id)
        {
            var item = repositoryP.GetAll();

            var newItem = item.Select(i => new ProductView()
            {
                CategoryId = i.CategoryId,
                Id = i.Id,
                Name = i.Name,
                Description = i.Description.Length > 50 ? i.Description.Substring(0, 20) + "..." : i.Description,
                Price = i.Price,
                Image = i.Image

            }).AsQueryable();

            if (id != null)
            {
                newItem = newItem.Where(i => i.CategoryId == id);
            }
            newItem.ToList();

            return View(newItem);
        }

        public PartialViewResult CategoryList()
        {
            var item = repositoryC.GetAll();

            return PartialView(item);
        }

        public ActionResult ProductDetail(int id)
        {
            var item = repositoryP.GetOne(id);

            return View(item);
        }
    }
}