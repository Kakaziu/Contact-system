using ContactSystem.Data;
using ContactSystem.Models;
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

            if (contact == null) throw new Exception("Contato não encontrado.");

            return contact;
        }

        public async Task<Contact> Update(Contact contact, int id)
        {
            var newContact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            if (newContact == null) throw new Exception("Contato não encontrado.");

            newContact.Name = contact.Name;
            newContact.Email = contact.Email;
            newContact.PhoneNumber = contact.PhoneNumber;
            _dbContext.Update(newContact);
            await _dbContext.SaveChangesAsync();

            return newContact;
        } 

        public async Task<Contact> Remove(int id)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            if (contact == null) throw new Exception("Contato não encontrado.");

            _dbContext.Remove(contact);
            await _dbContext.SaveChangesAsync();

            return contact;
        }
    }
}
