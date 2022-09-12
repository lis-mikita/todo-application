using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo_aspnetmvc_ui.Contexts;
using todo_aspnetmvc_ui.Models;
using todo_aspnetmvc_ui.ViewModels;
using todo_domain_entities.EntitiesBL;
using todo_domain_entities.Interfaces;

namespace todo_aspnetmvc_ui.Controllers
{
    /// <summary>
    /// Provides display of logging and registration pages.
    /// </summary>
    public class AccountController : Controller
    {
        private const string KeyEmail = "Email";
        private const string KeyPassword = "Password";
        private readonly IBL _db;

        private Mapper MapperUser { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="bl">Session with database.</param>
        public AccountController(IBL bl)
        {
            _db = bl;

            var configUser = new MapperConfiguration(cfg =>
                cfg.CreateMap<UserBL, UserModel>().ReverseMap());
            MapperUser = new Mapper(configUser);
        }

        /// <summary>
        /// Return login page.
        /// </summary>
        /// <returns>Login view page.</returns>
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (Request.Cookies[KeyEmail] != null && Request.Cookies[KeyPassword] != null)
            {
                LoginModel model = new LoginModel
                {
                    Email = Request.Cookies[KeyEmail],
                    Password = Request.Cookies[KeyPassword],
                };

                return await Login(model);
            }

            if (new TypeBrowser(HttpContext).IsMobileDeviceBrowser())
            {
                return View("Login.Mobile");
            }

            return View();
        }

        /// <summary>
        /// Authorize user in application.
        /// </summary>
        /// <param name="model">Input data from user.</param>
        /// <returns>Index or Login page.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (Request.Cookies[KeyEmail] != null && Request.Cookies[KeyPassword] != null)
            {
                model.Email = Request.Cookies[KeyEmail];
                model.Password = Request.Cookies[KeyPassword];
            }
            else if (model.Email != null && model.Password != null)
            {
                CookieOptions cookieOptions = new CookieOptions();
                Response.Cookies.Append(KeyEmail, model.Email);
                Response.Cookies.Append(KeyPassword, model.Password);

                const int DAYS_REMEMBER = 30;
                cookieOptions.Expires = model.RememberMe ? DateTime.Now.AddDays(DAYS_REMEMBER) : DateTime.Now.AddDays(-1);

                Response.Cookies.Delete(KeyEmail, cookieOptions);
                Response.Cookies.Delete(KeyPassword, cookieOptions);
            }
            else
            {
                // Not use
            }

            if (ModelState.IsValid)
            {
                UserModel user = MapperUser.Map<UserModel>(_db.GetUsers().FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password));
                if (user != null)
                {
                    await AuthenticateAsync(model.Email);

                    return RedirectToAction("Index", "Home");
                }

                const string ErrorMessage = "Login and(or) password is incorrect";
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            if (new TypeBrowser(HttpContext).IsMobileDeviceBrowser())
            {
                return View("Login.Mobile", model);
            }

            return View(model);
        }

        /// <summary>
        /// Return register page.
        /// </summary>
        /// <returns>Register view page.</returns>
        [HttpGet]
        public IActionResult Register()
        {
            if (new TypeBrowser(HttpContext).IsMobileDeviceBrowser())
            {
                return View("Register.Mobile");
            }

            return View();
        }

        /// <summary>
        /// Register in system.
        /// </summary>
        /// <param name="model">Input register form from user.</param>
        /// <returns>Index or Register pages.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = MapperUser.Map<UserModel>(_db.GetUsers().FirstOrDefault(u => u.Email == model.Email));
                if (user == null)
                {
                    // add user to bd
                    UserModel userNew = new UserModel
                    {
                        Email = model.Email,
                        Password = model.Password,
                        Name = model.Name,
                    };

                    _db.AddUser(MapperUser.Map<UserBL>(userNew));

                    await AuthenticateAsync(model.Email);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    const string ErrorMessage = "Login and(or) password is incorrect";
                    ModelState.AddModelError(string.Empty, ErrorMessage);
                }
            }

            if (new TypeBrowser(HttpContext).IsMobileDeviceBrowser())
            {
                return View("Register.Mobile", model);
            }

            return View(model);
        }

        /// <summary>
        /// User logout.
        /// </summary>
        /// <returns>Login page.</returns>
        public async Task<IActionResult> Logout()
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1),
            };

            Response.Cookies.Delete(KeyEmail, cookieOptions);
            Response.Cookies.Delete(KeyPassword, cookieOptions);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }

        private async Task AuthenticateAsync(string userName)
        {
            // create one claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
            };

            // create object ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            // set authentication cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
