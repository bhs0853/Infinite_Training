using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Models;
using Project.Repository;

namespace Project.Services
{
    public class InternetBankingService
    {
        private IInternetBankingRepository _ibrepo;

        public InternetBankingService(IInternetBankingRepository _ibrepo)
        {
            this._ibrepo = _ibrepo;
        }

        public (int admin_id, string message) AdminLogin(string email, string password)
        {
            return _ibrepo.AdminLogin(email, password);
        }

        public (int accountNumber, string message, string lastLogin) CustomerLogin(string email, string password)
        {
            return _ibrepo.CustomerLogin(email, password);
        }

        public string ChangeLoginPassword(int accountNumber, string oldPassword, string newPassword)
        {
            if (oldPassword == newPassword)
            {
                return "New password cannot be same as old password";
            }
            return _ibrepo.ChangeLoginPassword(accountNumber, oldPassword, newPassword);
        }

        public string ChangeTransactionPassword(int accountNumber, string oldPassword, string newPassword)
        {
            if (oldPassword == newPassword)
            {
                return "New password cannot be same as old password";
            }
            return _ibrepo.ChangeTransactionPassword(accountNumber, oldPassword, newPassword);
        }

        public string CreateDebitCard(int accountNumber)
        {
            return _ibrepo.CreateDebitCard(accountNumber);
        }
        public string CreateInternetBanking(int accountNumber, string loginPassword, string transactionPassword)
        {
            return _ibrepo.CreateInternetBanking(accountNumber, loginPassword, transactionPassword);
        }
        public string ResetLoginPassword(string email, string newPassword)
        {
            return _ibrepo.ResetLoginPassword(email, newPassword);
        }
    }
}