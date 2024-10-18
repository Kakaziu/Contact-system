using ContactSystem.Helpers;
using ContactSystem.Models;
using ContactSystem.Models.ViewModels;
using ContactSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContactSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService;
        private readonly Session _session;

        public LoginController(UserService userService, Session session)
        {
            _userService = userService;
            _session = session;
        }

        public IActionResult Index()
        {
            if (_session.GetUserSession() != null) return RedirectToAction("Index", "Contact");

            return View();
        }

        public IActionResult Logout()
        {
            _session.RemoveUserSession();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Entry(LoginModel loginModel)
        {
            try
            {
                User user = await _userService.FindByLogin(loginModel.Login);

                if(user.IsPasswordValid(loginModel.Password))
                {
                    _session.CreateUserSession(user);
                    return RedirectToAction("Index", "Contact");
                }

                return RedirectToAction(nameof(Error), new { message = "Usuário inválido" });
            }
            catch (ApplicationException ex) 
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel()
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
