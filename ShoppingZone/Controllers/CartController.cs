using ShoppingZone.Business.Repository;
using ShoppingZone.Data.Entity;
using ShoppingZone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ShoppingZone.Controllers
{
    public class CartController : Controller
    {
        GenericRepository<Product> repositoryP = new GenericRepository<Product>();
        GenericRepository<Order> repositoryO = new GenericRepository<Order>();

        [Authorize]
        public ActionResult CartPage()
        {
            return View(GetCart());
        }

        [Authorize]
        public ActionResult AddToCart(int id)
        {
            var product = repositoryP.GetOne(id);

            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }

            return RedirectToAction("CartPage");
        }

        public ActionResult RemoveToCart(int id)
        {
            var product = repositoryP.GetOne(id);

            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }

            return RedirectToAction("CartPage");
        }

        [Authorize]
        public ActionResult Checkout()
        {
            return View(new Shipping());
        }

        [HttpPost]
        public ActionResult Checkout(Shipping model)
        {
            var cart = GetCart();

            if (cart.cartLines.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                SaveOrder(cart, model);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(model);
            }
        }

        private void SaveOrder(Cart cart, Shipping model)
        {
            var order = new Order();

            order.OrderNumber = "A" + (new Random().Next(111111, 999999));
            order.Total = cart.Total();
            order.OrderDate = DateTime.Now;
            order.Username = User.Identity.Name;
            order.AddressName = model.AddressName;
            order.Address = model.Address;

            order.OrderLines = new List<OrderLine>();

            foreach (var item in cart.cartLines)
            {
                var orderLine = new OrderLine();

                orderLine.Quantity = item.Quantity;
                orderLine.Price = item.Quantity * item.Product.Price;
                orderLine.ProductId = item.Product.Id;
                orderLine.Image = item.Image;

                order.OrderLines.Add(orderLine);
            }

            repositoryO.NewOne(order);
        }

        public PartialViewResult CartSummary()
        {
            return PartialView(GetCart());
        }

        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }
    }
}