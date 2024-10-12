using ContactSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Data
{
    public class ContactSystemDBContext : DbContext
    {
        public ContactSystemDBContext(DbContextOptions<ContactSystemDBContext> options)
        : base(options)
        { 
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
