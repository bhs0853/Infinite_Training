using assignment1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;

namespace assignment1.Repository
{
    public class ContactRepository : IContactRepository
    {
        ContactContext db;

        public ContactRepository()
        {
            db = new ContactContext();
        }
        public Task CreateAsync(Contact contact)
        {
            db.contacts.Add(contact);
            db.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(long Id)
        {
            var getContact = db.contacts.Find(Id);
            db.contacts.Remove(getContact);
            db.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return (await db.contacts.ToListAsync());
        }
    }
}