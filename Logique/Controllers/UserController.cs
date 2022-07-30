using Logique.Models.Entities;
using Logique.Models.Helpers;
using Logique.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TeleCentre.Web.Portal.Models;

namespace Logique.Controllers
{
    public class UsersController : Controller
    {
        private IUserInterface _user;

        public UsersController(IUserInterface user)
        {
            _user = user;
        }

        // GET: Users
        [Authorize]
        public ActionResult Index()
        {
            var users = _user.GetUsers();
            return View(users);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            var obj = _user.ExistUser(user.Email);
            if (obj == null)
            {
                if (ModelState.IsValid)
                {
                    var userAdd = _user.Register(user);
                    Console.WriteLine(userAdd);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error Occured! Try again!!");
                }
            }
            else
            {
                ModelState.AddModelError("", "User exists ,Please login with your password");
            }
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            var obj = _user.Login(user);
            if (obj != null)
            {
                HttpContext.Session.SetString("email", obj.Email);
                HttpContext.Session.SetString("lastname", obj.LastName);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Userid or password wrong");
            }

            return View(user);
        }

        [Authorize]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}