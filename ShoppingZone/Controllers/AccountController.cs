using ShoppingZone.Business.Repository;
using ShoppingZone.Data.Entity;
using ShoppingZone.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace ShoppingZone.Controllers
{
    public class AccountController : Controller
    {
        GenericRepository<User> repositoryU = new GenericRepository<User>();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var userList = repositoryU.GetAll();
            var login = userList.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

            if (ModelState.IsValid)
            {
                if (login != null)
                {
                    FormsAuthentication.SetAuthCookie(login.UserName, false);
                    Session["UserName"] = login.UserName;

                    return RedirectToAction("ProfilePage", "Profile");
                }
                else
                {
                    return View(user);
                }
            }
            else
            {
                return View(user);
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        public ActionResult Register(User user, HttpPostedFileBase imageFile)
        {
            var userList = repositoryU.GetAll();
            var login = userList.FirstOrDefault(x => x.UserName == user.UserName);

            if (ModelState.IsValid && imageFile != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(imageFile.InputStream))
                {
                    imageData = binaryReader.ReadBytes(imageFile.ContentLength);
                }

                user.ProfileImage = imageData;

                if (ModelState.IsValid)
                {
                    if (login != null)
                    {
                        ModelState.AddModelError("UserName", "This username already taken");
                        return View(user);
                    }
                    else
                    {
                        user.Role = "User";
                        user.JoinTime = DateTime.Now;

                        repositoryU.NewOne(user);
                        return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    return View(user);
                }
            }
            return View(user);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();

            return RedirectToAction("Home", "Home");
        }

        [Authorize]
        public PartialViewResult ChoiceList()
        {
            return PartialView();
        }

        [Authorize]
        public PartialViewResult AdminChoice()
        {
            var login = User.Identity.Name;
            var userList = repositoryU.GetAll();
            var user = userList.FirstOrDefault(x => x.UserName == login);

            return PartialView(user);
        }

        public PartialViewResult LoginStatus()
        {
            bool isLoggedIn = HttpContext.User.Identity.IsAuthenticated;

            return PartialView(isLoggedIn);
        }

        public PartialViewResult LoginPhoto()
        {
            var login = User.Identity.Name;
            var userList = repositoryU.GetAll();
            var user = userList.FirstOrDefault(x => x.UserName == login);

            return PartialView(user);
        }
    }
}