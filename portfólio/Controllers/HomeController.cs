using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using portfólio.DAO.PostManager;
using portfólio.DAO.RepositoryUser;
using portfólio.Helpers;
using portfólio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace portfólio.Controllers
{
    [Logado]
    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository _postRepository;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository, IPostRepository postRepository)
        {
            _logger = logger;
            _postRepository = postRepository;
            this.userRepository = userRepository;
        }
        public IActionResult Index()
        {
                return View(_postRepository.GetPosts());
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
