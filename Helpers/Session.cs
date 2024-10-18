using ContactSystem.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContactSystem.Helpers
{
    public class Session : ISession
    {
        private readonly IHttpContextAccessor _httpContext;

        public Session(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public void CreateUserSession(User user)
        {
            string value = JsonSerializer.Serialize(user);

            _httpContext.HttpContext.Session.SetString("LoggedUserSession", value);
        }

        public User? GetUserSession()
        {
            string value = _httpContext.HttpContext.Session.GetString("LoggedUserSession");

            if (string.IsNullOrEmpty(value)) return null;

            return JsonSerializer.Deserialize<User>(value);
        }

        public void RemoveUserSession()
        {
            _httpContext.HttpContext.Session.Remove("LoggedUserSession");
        }
    }
}
