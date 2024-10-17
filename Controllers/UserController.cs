using ContactSystem.Models;
using ContactSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService) 
        { 
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            User newUser = await _userService.Insert(user);

            return RedirectToAction("Index", "Contact");
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.FindAll();

            return View(users);
        }
    }
}
