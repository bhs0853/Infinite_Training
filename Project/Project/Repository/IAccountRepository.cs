using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public interface IAccountRepository
    {
        string RegisterAccount(RegisterAccount account);
        string CreateAccount(int service_reference_number, int id);
        (Account acc, string message) GetAccountDetails(int accountNo);

    }
}
