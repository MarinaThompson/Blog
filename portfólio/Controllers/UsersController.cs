using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using portfólio.DAO;
using portfólio.DAO.FileManager;
using portfólio.DAO.RepositoryUser;
using portfólio.Helpers;
using portfólio.Models;
using portfólio.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portfólio.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IFileManager _fileManager; 

        public UsersController(IUserRepository userRepository, IFileManager fileManager)
        {
            this.userRepository = userRepository;
            _fileManager = fileManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);            
            }
            vm = new RegisterViewModel()
            {
                Username = vm.Username,
                Email = vm.Email,
                Password = vm.Password,
                Photo = vm.Photo
            };
            userRepository.Register(vm);        
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            if (vm.Email != "" && vm.Password.Trim().Length > 0)
            {
                var user = userRepository.Login(vm.Email, vm.Password);

                if(user != null)
                {
                    var expira = DateTimeOffset.UtcNow.AddHours(3);
                    this.HttpContext.Response.Cookies.Append("user_login", user.ID.ToString(), new CookieOptions
                    {
                        Expires = expira,
                        HttpOnly = true
                    });

                    this.HttpContext.Response.Cookies.Append("user_login_ID", user.ID.ToString(), new CookieOptions
                    {
                        Expires = expira,
                        HttpOnly = true,
                    });

                    Response.Redirect("/");
                }
                else
                {
                    ViewBag.erro = "Usuário ou senha inválidos";
                }
            }
            return View(vm);            
        }

        public IActionResult Logout()
        {
            this.HttpContext.Response.Cookies.Append("user_login", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(-1),
                HttpOnly = true
            });
            return RedirectToAction("Login");
        }
      
    }
}
