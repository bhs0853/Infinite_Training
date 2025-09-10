using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Models;
using Project.Repository;

namespace Project.Services
{
    public class AccountService
    {
        private IAccountRepository _accountRepo;

        public AccountService(IAccountRepository _accountRepo)
        {
            this._accountRepo = _accountRepo;
        }

        public string CreateAccount(int service_reference_number, int id)
        {
            return _accountRepo.CreateAccount(service_reference_number, id);
        }

        public (Account acc, string message) GetAccountDetails(int accountNo)
        {
            return _accountRepo.GetAccountDetails(accountNo);
        }

        public string RegisterAccount(RegisterAccount account)
        {
            if (string.IsNullOrEmpty(account.Aadhar))
            {
                return "Aadhar is required";
            }
            return _accountRepo.RegisterAccount(account);
        }
    }
}