using ShoppingZone.Business.Repository;
using ShoppingZone.Data.Entity;
using ShoppingZone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingZone.Controllers
{
    public class ProfileController : Controller
    {
        GenericRepository<User> repositoryU = new GenericRepository<User>();
        GenericRepository<Order> repositoryO = new GenericRepository<Order>();
        GenericRepository<OrderLine> repositoryOL = new GenericRepository<OrderLine>();


        [Authorize]
        public ActionResult ProfilePage()
        {
            var login = User.Identity.Name;
            var userList = repositoryU.GetAll();
            var user = userList.FirstOrDefault(x => x.UserName == login);

            return View(user);
        }

        [Authorize]
        public ActionResult Order()
        {
            var username = User.Identity.Name;
            var order = repositoryO.GetAll();

            var item = order.Where(i => i.Username == username).Select(i => new OrderView()
            {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                Total = i.Total,
                OrderDate = i.OrderDate,
                Username = i.Username,
                AddressName = i.AddressName,
                Address = i.Address

            }).OrderByDescending(i => i.OrderDate).ToList();

            return View(item);
        }

        public ActionResult OrderDetail(int id)
        {
            var order = repositoryO.GetAll();

            var item = order.Where(i => i.Id == id).Select(i => new OrderDetail()
            {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                Total = i.Total,
                OrderDate = i.OrderDate,
                AddressName = i.AddressName,
                Address = i.Address,
                OrderLines = i.OrderLines.Select(x => new OrderLineDetail()
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Image = x.Image

                }).ToList()
            }).FirstOrDefault();

            return View(item);
        }
    }
}