using System;

namespace assignment5
{
    /*
     * 1. You have a class which has methods for transaction for a banking system. (created earlier)
	    Define your own methods for deposit money, withdraw money and balance in the account.
	    Write your own new application Exception class called InsuffientBalanceException. 
	    This new Exception will be thrown in case of withdrawal of money from the account where customer doesn’t have sufficient balance.
     */
    class Accounts
    {
        int accountNo { get; }
        public string name { get; set; }
        public string accountType { get; set; }
        int balance { get; set; }

        public Accounts(int accountNo, string name, string accountType, int balance)
        {
            this.accountNo = accountNo;
            this.name = name;
            this.accountType = accountType;
            this.balance = balance;
        }
        public void Credit(int depositAmount)
        {
            this.balance += depositAmount;
            Console.WriteLine($"Deposited: Rs {depositAmount} The Available balance is: Rs {this.balance}");
            Console.WriteLine("*********************************************");
        }
        public void Debit(int debitAmount)
        {
            if (debitAmount > this.balance)
                throw new InsufficientBalanceException($"Insufficient funds. Available Balance: Rs {this.balance}");

            this.balance -= debitAmount;
            Console.WriteLine($"withdrawn: Rs {debitAmount} and The Available balance is: Rs {this.balance}");
            Console.WriteLine("*********************************************");
        }

        public void ShowBalance()
        {
            Console.WriteLine($"The Available balance is: Rs {this.balance}");
            Console.WriteLine("*********************************************");
        }

        public void ShowAccountDetails()
        {
            Console.WriteLine("*************** Account Details ***************");
            Console.WriteLine($"Account No: {accountNo}");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"AccountType: {accountType}");
            Console.WriteLine($"Balance: {balance}");
            Console.WriteLine("*********************************************");
        }
    }
    class Question1
    {
        static void Main(string[] args)
        {
            Accounts account = null;
            Console.WriteLine("class Accounts:");
            while (account == null)
            {
                try
                {
                    Console.Write("Enter the Account no: ");
                    int accNo = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter the Account holder name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter the Account type: ");
                    string accountType = Console.ReadLine();

                    account = new Accounts(accNo, name, accountType, 0);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("Please Enter b for balance c for credit, d for debit, s for account details, any other key to exit");
                    char transactionType = char.Parse(Console.ReadLine());
                    if (transactionType == 'd')
                    {
                        Console.Write("Enter the Amount to be Debited: ");
                        int debitAmount = Convert.ToInt32(Console.ReadLine());
                        account.Debit(debitAmount);
                    }
                    else if (transactionType == 'c')
                    {
                        Console.Write("Enter the Amount to be Credited: ");
                        int creditAmount = Convert.ToInt32(Console.ReadLine());
                        account.Credit(creditAmount);
                    }
                    else if (transactionType == 's')
                        account.ShowAccountDetails();
                    else if (transactionType == 'b')
                        account.ShowBalance();
                    else
                        break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("******************** END OF TRANSACTION *************************");
            Console.Read();
        }
    }
}
