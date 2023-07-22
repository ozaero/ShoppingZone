using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingZone.Business.Repository;
using ShoppingZone.Data.Entity;
using ShoppingZone.Models;

namespace ShoppingZone.Controllers
{
    public class Admin2Controller : Controller
    {
        GenericRepository<Category> repositoryC;

        public Admin2Controller()
        {
            repositoryC = new GenericRepository<Category>();
        }

        public ActionResult Index()
        {
            var category = repositoryC.GetAll();

            return View(category);
        }

        public ActionResult Details(int id)
        {
            var category = repositoryC.GetOne(id);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                repositoryC.NewOne(category);

                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Edit(int id)
        {
            var category = repositoryC.GetOne(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                repositoryC.Put(category);

                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(int id)
        {
            var product = repositoryC.GetOne(id);

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repositoryC.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
