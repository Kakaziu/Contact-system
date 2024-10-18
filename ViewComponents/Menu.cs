using ContactSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ContactSystem.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string session = HttpContext.Session.GetString("LoggedUserSession");

            if (session == null) return null;

            User user = JsonSerializer.Deserialize<User>(session);

            return View(user);
        }
    }
}
