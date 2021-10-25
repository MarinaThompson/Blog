using Microsoft.AspNetCore.Mvc;
using portfólio.DAO.FileManager;
using portfólio.DAO.PostManager;
using portfólio.DAO.RepositoryUser;
using portfólio.Helpers;
using portfólio.Models;
using portfólio.ModelViews;
using System.Threading.Tasks;

namespace portfólio.Controllers
{
    public class PostsController : Controller
    {
        private readonly IFileManager _fileManager;
        private readonly IPostRepository _postRepository;

        public PostsController(IFileManager fileManager, IPostRepository postRepository)
        {
            _fileManager = fileManager;
            _postRepository = postRepository;
        }

        public IActionResult MyPosts()
        {
            var id = TempData["usuarioLogado"].ToString();
            return View(_postRepository.MyPosts(id));
        }

        [Logado]
        [HttpPost]
        public IActionResult DeletePost(Post post)
        {
            if (_postRepository.DeletePost(post)) return RedirectToAction("Index", "Home");
            return RedirectToAction("MyPosts", "Home");
        }

        [Logado]
        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }
        [Logado]
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var post = new Post()
            {
                ID = vm.ID,
                Subtitle = vm.Subtitle,
                UserID = vm.UserID
            };

            if (vm.Image == null)
                post.Image = vm.CurrentImage;
            else
                post.Image = await _fileManager.SaveImage(vm.Image);
            _postRepository.CreatePost(post);
            return RedirectToAction("Index", "Home");
        }

        [Logado]
        [HttpGet]
        public IActionResult EditPost()
        {
            return View();
        }

        [Logado]
        [HttpPost]
        public async Task<IActionResult> EditPost(PostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var post = new Post()
            {
                ID = vm.ID,
                Subtitle = vm.Subtitle,
                UserID = vm.UserID
            };

            if (vm.Image == null)
                post.Image = vm.CurrentImage;
            else
                post.Image = await _fileManager.SaveImage(vm.Image);

            _postRepository.UpdatePost(post);
            return RedirectToAction("Index", "Home");
        }

    }
}
