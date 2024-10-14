using ContactSystem.Data;
using ContactSystem.Models;
using ContactSystem.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Services
{
    public class UserService
    {
        private readonly ContactSystemDBContext _dbContext;

        public UserService(ContactSystemDBContext dbContext)
        {
            _dbContext = dbContext;
        }

         public async Task<List<User>> FindAll()
        {
            var users = await _dbContext.Users.ToListAsync();

            return users;
        }

        public async Task<User> FindById(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<User> Insert(User user)
        {
            user.CreatedAt = DateTime.Now;
            await _dbContext.Users.AddAsync(user);
            await _dbContext.Users.SingleAsync();

            return user;
        }

        public async Task<User> Remove(int id)
        {
            var user = await FindById(id);

            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> Update(User user, int id)
        {
            if (!_dbContext.Users.Any(x => x.Id == id))
                throw new NotFoundException("Id not found");

            User userDB = await FindById(id);

            userDB.Name = user.Name;
            userDB.Email = user.Email;
            userDB.Login = user.Login;
            userDB.UpdateAt = DateTime.Now;

            try
            {
                _dbContext.Users.Update(userDB);
                await _dbContext.SaveChangesAsync();

                return userDB;
            } catch(DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
