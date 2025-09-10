using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public interface IInternetBankingRepository
    {
        string CreateInternetBanking(int accountNumber, string loginPassword, string transactionPassword);
        (int admin_id, string message) AdminLogin(string email, string password);
        (int accountNumber, string message, string lastLogin) CustomerLogin(string email, string password);

        string ChangeLoginPassword(int accountNumber, string oldPassword, string newPassword);
        string ChangeTransactionPassword(int accountNumber, string oldPassword, string newPassword);
        string ResetLoginPassword(string email, string newPassword);
        string CreateDebitCard(int accountNumber);
    }
}
