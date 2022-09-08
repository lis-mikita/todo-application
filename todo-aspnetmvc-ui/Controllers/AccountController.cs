using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
using System;

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
        public async Task<IActionResult> Login()
        {
            if (Request.Cookies["Email"] != null && Request.Cookies["Password"] != null)
            {
                LoginModel model = new LoginModel();
                model.Email = Request.Cookies["Email"].ToString();
                model.Password = Request.Cookies["Password"].ToString();
                return await Login(model);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (Request.Cookies["Email"] != null && Request.Cookies["Password"] != null)
            {
                model.Email = Request.Cookies["Email"].ToString();
                model.Password = Request.Cookies["Password"].ToString();
            }
            else if (model.Email != null && model.Password != null)
            {
                CookieOptions cookieOptions = new CookieOptions();
                Response.Cookies.Append("Email", model.Email);
                Response.Cookies.Append("Password", model.Password);

                if (model.RememberMe)
                {
                    cookieOptions.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Delete("Email", cookieOptions);
                    Response.Cookies.Delete("Password", cookieOptions);
                }
                else
                {
                    cookieOptions.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Delete("Email", cookieOptions);
                    Response.Cookies.Delete("Password", cookieOptions);
                }
            }
            else
            {
                // Not use
            }

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
                        UserModel userNew = new UserModel
                        {
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
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Delete("Email", cookieOptions);
            Response.Cookies.Delete("Password", cookieOptions);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
