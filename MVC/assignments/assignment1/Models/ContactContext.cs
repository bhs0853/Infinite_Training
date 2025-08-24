using System.Data.Entity;

namespace assignment1.Models
{
    public class ContactContext : DbContext
    {
        public ContactContext() : base("name = connectionstr") { }
        public DbSet<Contact> contacts { get; set; }
    }
}