using ContactSystem.Data;
using ContactSystem.Models;
using ContactSystem.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Services
{
    public class ContactService
    {
        private readonly ContactSystemDBContext _dbContext;

        public ContactService(ContactSystemDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Contact>> FindAll()
        {
            var contacts = await _dbContext.Contacts.ToListAsync();

            return contacts;
        }

        public async Task<Contact> Insert(Contact contact)
        {
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            return contact;
        }

        public async Task<Contact> FindById(int id)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            return contact;
        }

        public async Task<Contact> Update(Contact contact, int id)
        {
            if (!_dbContext.Contacts.Any(x => x.Id == id))
                throw new NotFoundException("Id not found.");

            try
            {
                _dbContext.Update(contact);
                await _dbContext.SaveChangesAsync();

                return contact;
            } catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        } 

        public async Task<Contact> Remove(int id)
        {
            var contact = await FindById(id);

            _dbContext.Remove(contact);
            await _dbContext.SaveChangesAsync();

            return contact;
        }
    }
}
