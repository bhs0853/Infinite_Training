using System;

namespace assignment3
{
    /*
     * 1. Create a class called Accounts which has data members/fields like Account no, Customer name, Account type, Transaction type (d/w), amount, balance
      
     * write a function that updates the balance depending upon the transaction type

	    -If transaction type is deposit call the function credit by passing the amount to be deposited and update the balance 
            function  Credit(int amount) 

	    -If transaction type is withdraw call the function debit by passing the amount to be withdrawn and update the balance
            Debit(int amt) function 

        -Pass the other information like Account no, customer name, acc type through constructor

        -write and call the show data method to display the values.
     */
    enum accountType { SAVINGS, CURRENT, SALARY };
    class Accounts
    {
        int accountNo { get; }
        public String name { get; set; }
        public String accountType { get; set; }
        int amount { get; set; }

        public Accounts(int accountNo, String name, String accountType, int amount)
        {
            this.accountNo = accountNo;
            this.name = name;
            this.accountType = accountType;
            this.amount = amount;
        }
        public void Credit(int depositAmount)
        {
            this.amount += depositAmount;
            Console.WriteLine($"The Available balance is: {this.amount}");
            Console.WriteLine("*********************************************");
        }
        public bool Debit(int debitAmount)
        {
            if (debitAmount > this.amount)
            {
                Console.WriteLine($"Insufficient funds. Available Balance: {this.amount}");
                return false;
            }
            this.amount -= debitAmount;
            Console.WriteLine($"The Available balance is: {this.amount}");
            Console.WriteLine("*********************************************");
            return true;
        }

        public void ShowAccountDetails()
        {
            Console.WriteLine("*************** Account Details ***************");
            Console.WriteLine($"Account No: {accountNo}");
            Console.WriteLine($"name: {name}");
            Console.WriteLine($"accountType: {accountType}");
            Console.WriteLine($"amount: {amount}");
            Console.WriteLine("*********************************************");
        }
    }
    class AccountsQuestion
    {
        static void Main(string[] args)
        {
            Console.WriteLine("class Accounts:");
            Console.WriteLine("Enter the Account no:");
            int accNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Account holder name:");
            String name = Console.ReadLine();
            Console.WriteLine("Enter the Account type:");
            String accountType = Console.ReadLine();

            Accounts bhs = new Accounts(accNo, name, accountType, 0);
            char ch = ' ';
            do
            {
                Console.WriteLine("Please Enter c for credit, d for credit, s for account details, any other key to exit");
                ch = char.Parse(Console.ReadLine());
                if (ch == 'd')
                {
                    Console.WriteLine("Enter the Debit Account: ");
                    int debitAmount = Convert.ToInt32(Console.ReadLine());
                    bhs.Debit(debitAmount);
                }
                else if (ch == 'c')
                {
                    Console.WriteLine("Enter the Credit Account: ");
                    int creditAmount = Convert.ToInt32(Console.ReadLine());
                    bhs.Credit(creditAmount);
                }
                else if (ch == 's')
                    bhs.ShowAccountDetails();
            } while (ch == 'c' || ch == 'd' || ch == 's');
            Console.WriteLine("******************** END OF TRANSACTION *************************");
            Console.Read();
        }
    }
}
