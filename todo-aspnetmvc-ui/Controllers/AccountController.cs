using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using todo_aspnetmvc_ui.Models;
using todo_domain_entities.Entities;
using todo_domain_entities;
using System.Linq;
using AutoMapper;
using todo_domain_entities.EntitiesBL;
using todo_aspnetmvc_ui.ViewModels;

namespace todo_aspnetmvc_ui.Controllers
{
    public class AccountController : Controller
    {
        private Mapper MapperUser { get; }
        public AccountController()
        {
            var configUser = new MapperConfiguration(cfg =>
                cfg.CreateMap<UserBL, UserModel>().ReverseMap());
            MapperUser = new Mapper(configUser);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            using (var db = new BL())
            {
                if (ModelState.IsValid)
                {
                    UserModel user = MapperUser.Map<UserModel>(db.GetUsers().FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password));
                    if (user != null)
                    {
                        await Authenticate(model.Email);

                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Login and(or) password is incorrect");
                }
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            using (var db = new BL())
            {
                if (ModelState.IsValid)
                {
                    UserModel user = MapperUser.Map<UserModel>(db.GetUsers().FirstOrDefault(u => u.Email == model.Email));
                    if (user == null)
                    {
                        // add user to bd
                        UserModel userNew = new UserModel { 
                            Email = model.Email, 
                            Password = model.Password,
                            Name = model.Name
                        };

                        db.AddUser(MapperUser.Map<UserBL>(userNew));

                        await Authenticate(model.Email);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("", "Login and(or) password is incorrect");
                }
                return View(model);
            }
        }

        private async Task Authenticate(string userName)
        {
            // create one claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            // create object ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            // set authentication cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
