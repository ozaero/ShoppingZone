using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingZone.Business.Repository;
using ShoppingZone.Data.Entity;
using ShoppingZone.Models;

namespace ShoppingZone.Controllers
{
    public class AdminController : Controller
    {
        GenericRepository<Product> repositoryP;
        GenericRepository<User> repositoryU;

        public AdminController()
        {
            repositoryP = new GenericRepository<Product>();
            repositoryU = new GenericRepository<User>();
        }

        public ActionResult Index()
        {
            var product = repositoryP.GetAll();

            return View(product);
        }

        public ActionResult Details(int id)
        {
            var product = repositoryP.GetOne(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,Name,Description,Price")] Product product, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    byte[] imageData = new byte[imageFile.ContentLength];
                    using (var binaryReader = new BinaryReader(imageFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(imageFile.ContentLength);
                    }
                    product.Image = imageData;
                }

                repositoryP.NewOne(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }


        public ActionResult Edit(int id)
        {
            var product = repositoryP.GetOne(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = repositoryP.GetOne(product.Id);

                if (existingProduct != null)
                {
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        byte[] imageData = null;
                        using (var binaryReader = new BinaryReader(imageFile.InputStream))
                        {
                            imageData = binaryReader.ReadBytes(imageFile.ContentLength);
                        }
                        existingProduct.Image = imageData;
                    }

                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.CategoryId = product.CategoryId;

                    repositoryP.Put(existingProduct);

                    return RedirectToAction("Index");
                }
            }

            return View(product);
        }


        public ActionResult Delete(int id)
        {
            var product = repositoryP.GetOne(id);

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repositoryP.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult UserList()
        {
            var userList = repositoryU.GetAll();

            return View(userList);
        }
    }
}
