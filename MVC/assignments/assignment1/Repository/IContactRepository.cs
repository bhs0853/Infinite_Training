using assignment1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace assignment1.Repository
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();
        Task CreateAsync(Contact contact);
        Task DeleteAsync(long Id);
    }
}
