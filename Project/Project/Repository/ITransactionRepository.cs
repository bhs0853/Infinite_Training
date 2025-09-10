using Project.Dto;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public interface ITransactionRepository
    {
        string AddPayee(Payee payee);
        (Payee payee, string message) GetPayee(int id);
        (List<Payee> payees, string message) GetAllPayees(int accountNumber);
        (List<Transaction_Details> transactions, string message) GetStatement(int accountNumber, DateTime fromDate, DateTime toDate);

        (Transaction_Details txnDetails, string message) MakeTransaction(int accountNumber, TransactionDto transaction);

    }
}
