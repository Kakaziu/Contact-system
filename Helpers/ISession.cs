using ContactSystem.Models;

namespace ContactSystem.Helpers
{
    public interface ISession
    {
        void CreateUserSession(User user);
        void RemoveUserSession();
        User? GetUserSession();
    }
}
