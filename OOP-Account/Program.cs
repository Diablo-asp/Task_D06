using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Principal;
using static System.Reflection.Metadata.BlobBuilder;

class Program
{
    static void Main()
    {
        
        // Accounts
        var accounts = new List<Account>();
        accounts.Add(new Account());
        accounts.Add(new Account("Larry"));
        accounts.Add(new Account("Moe", 2000));
        accounts.Add(new Account("Curly", 5000));

        AccountUtil.Deposit(accounts, 1000);
        AccountUtil.Withdraw(accounts, 2000);

        // Overload operator+
        Account accounts1 = new Account("Ahmed", 3400);
        Account accounts2 = new Account("Hassan", 5000);
        Console.WriteLine(accounts.Count);
        Account account3 = accounts1 + accounts2;
        Console.WriteLine(account3.Name + account3.Balance);



        //Savings
        var savAccounts = new List<SavingsAccount>();
        savAccounts.Add(new SavingsAccount());
        savAccounts.Add(new SavingsAccount("Superman"));
        savAccounts.Add(new SavingsAccount("Batman", 2000));
        savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

        AccountUtil.DepositSavings(savAccounts, 1000);
        AccountUtil.WithdrawSavings(savAccounts, 2000);

        //Checking
       var checAccounts = new List<CheckingAccount>();
        checAccounts.Add(new CheckingAccount());
        checAccounts.Add(new CheckingAccount("Larry2"));
        checAccounts.Add(new CheckingAccount("Moe2", 2000));
        checAccounts.Add(new CheckingAccount("Curly2", 5000));

        AccountUtil.DepositChecking(checAccounts, 1000);
        AccountUtil.WithdrawChecking(checAccounts, 2000);
        AccountUtil.WithdrawChecking(checAccounts, 2000);

        // Trust
        var trustAccounts = new List<TrustAccount>();
        trustAccounts.Add(new TrustAccount());
        trustAccounts.Add(new TrustAccount("Superman2"));
        trustAccounts.Add(new TrustAccount("Batman2", 2000));
        trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

        AccountUtil.DepositTrust(trustAccounts, 1000);
        AccountUtil.DepositTrust(trustAccounts, 6000);
        AccountUtil.WithdrawTrust(trustAccounts, 2000);
        AccountUtil.WithdrawTrust(trustAccounts, 3000);
        AccountUtil.WithdrawTrust(trustAccounts, 500);

        Console.WriteLine();
    }
}

public class Account
{
    public string Name { get; set; }
    public double Balance { get; set; }

    public Account(string name = "Unnamed Account", double balance = 0.0)
    {
        this.Name = name;
        this.Balance = balance;
    }

    public bool Deposit(double amount)
    {
        if (amount < 0)
            return false;
        else
        {
            Balance += amount;
            return true;
        }
    }

    public bool Withdraw(double amount)
    {
        if (Balance - amount >= 0)
        {
            Balance -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Account operator +(Account lhs, Account rhs)
    {
        Account account = new(lhs.Name.ToUpper() + " " + rhs.Name.ToUpper() + " " , lhs.Balance + rhs.Balance);
        return account;
    }
}

public class SavingsAccount : Account
{

    public  SavingsAccount(string name = "Unnamed Account", double balance = 0.0, double interestRate = 0) : base(name, balance)
    {
        InterestRate = interestRate;
    }

    public double InterestRate { get; set; }

    public new bool Deposit(double amount)
    {
        if (amount <= 0)
            return false;
        else
        {
            Balance += amount;
            return true;
        }
    }
    public new bool Withdraw(double amount)
    {
        
        if (Balance - amount >= 0 && (amount * InterestRate) <= Balance)
        {
            Balance -= amount + (amount * InterestRate);
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class CheckingAccount : Account
{
    public CheckingAccount(string name = "Unnamed Account", double balance = 0.0, double fee = 1.5) : base(name, balance)
    {
        Fee = fee;
    }

    public double Fee { get; set; }

    public new bool Deposit(double amount)
    {
        if (amount <= 0)
            return false;
        else
        {
            Balance += amount;
            return true;
        }
    }
    public new bool Withdraw(double amount)
    {
        if (Balance - amount >= 0 && Balance > (amount + Fee))
        {
            Balance -= (amount + Fee);
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class TrustAccount : Account
{
    public TrustAccount(string name = "Unnamed Account", double balance = 0.0, double interestRate = 50, double withdrawPerYear = 3, double maxWithdraw = 0.2) : base(name, balance)
    {
        InterestRate = interestRate;
        WithdrawPerYear = withdrawPerYear;
        MaxWithdraw = maxWithdraw;
    }

    public double InterestRate { get;private set; }
    private double WithdrawPerYear { get; set; }
    private double MaxWithdraw { get; set; }


    public new bool Deposit(double amount)
    {
        
        if (amount < 5000 && amount >= 0)
        {
            Balance += amount;
            return true;
        }
        else if (amount >= 5000)
        {
            Balance += (amount + InterestRate);
            return true;
        }
        else
        {            
            return false;
        }
    }

    public new bool Withdraw(double amount)
    {
        int withdrawCount = 0;
        if (Balance - amount >= 0 && withdrawCount < WithdrawPerYear && amount <= (Balance * MaxWithdraw))
        {
            Balance -= amount;
            withdrawCount++;
            return true;
        }
        else
        {
            return false;
        } 
    }
}


public static class AccountUtil
{
    // Utility helper functions for Account class
    
    public static void Deposit(List<Account> accounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Accounts =================================");
        foreach (var acc in accounts)
        {
            if (acc.Deposit(amount))
                Console.WriteLine($"Deposited {amount} to {acc}");
            else
                Console.WriteLine($"Failed Deposit of {amount} to {acc}");
        }
    }

    public static void Withdraw(List<Account> accounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
        foreach (var acc in accounts)
        {
            if (acc.Withdraw(amount))
                Console.WriteLine($"Withdrew {amount} from {acc}");
            else
                Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
        }
    }

    // Helper functions for SavingsAccount

    public static void DepositSavings(List<SavingsAccount> savAccounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Savings Accounts =================================");
        foreach (var acc in savAccounts)
        {
            if (acc.Deposit(amount))
                Console.WriteLine($"Deposited {amount} to {acc}");
            else
                Console.WriteLine($"Failed Deposit of {amount} to {acc}");
        }
    }

    public static void WithdrawSavings(List<SavingsAccount> savAccounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Savings Accounts ==============================");
        foreach (var acc in savAccounts)
        {
            if (acc.Withdraw(amount))
                Console.WriteLine($"Withdrew {amount} from {acc}");
            else
                Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
        }
    }

    // Helper functions for CheckingAccount
    public static void DepositChecking(List<CheckingAccount> checAccounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Checking Accounts =================================");
        foreach (var acc in checAccounts)
        {
            if (acc.Deposit(amount))
                Console.WriteLine($"Deposited {amount} to {acc}");
            else
                Console.WriteLine($"Failed Deposit of {amount} to {acc}");
        }
    }

    public static void WithdrawChecking(List<CheckingAccount> checAccounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Checking Accounts ==============================");
        foreach (var acc in checAccounts)
        {
            if (acc.Withdraw(amount))
                Console.WriteLine($"Withdrew {amount} from {acc}");
            else
                Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
        }
    }

    //// Helper functions for TrustAccount
    public static void DepositTrust(List<TrustAccount> accounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Trust Accounts =================================");
        foreach (var acc in accounts)
        {
            if (acc.Deposit(amount))
                Console.WriteLine($"Deposited {amount} to {acc}");
            else
                Console.WriteLine($"Failed Deposit of {amount} to {acc}");
        }
    }

    public static void WithdrawTrust(List<TrustAccount> accounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Trust Accounts ==============================");
        foreach (var acc in accounts)
        {
            if (acc.Withdraw(amount))
                Console.WriteLine($"Withdrew {amount} from {acc}");
            else
                Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
        }
    }
}