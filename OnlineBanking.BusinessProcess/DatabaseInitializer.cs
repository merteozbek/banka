using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using OnlineBanking.DataModel;
using OnlineBanking.DAL;

namespace OnlineBanking.BusinessProcess
{
    public class DatabaseInitializer : IDatabaseInitializer<BankContext>
    {
        public void InitializeDatabase(BankContext context)
        {
            Crypto crypto = new Crypto();
            bool dbExists;
            dbExists = context.Database.Exists();
            if (dbExists)
            {
                Bank bank =  context.Banks.Where(b => b.bankName == "KSJ Bank").FirstOrDefault<Bank>();
                if (bank == null)
                {
                    context.Banks.Add(new Bank() { bankName = "KSJ Bank", bankAddress = "1 Bank Street, New York, New York" });
                    Customer customerKen = new Customer() { customerName = "Ken Dinsmore", address = "123 Main Street, San Antonio TX", phone = "210-555-5555", email = "Ken@email.com", customerIdStatus = "V" };
                    Customer customerSangam = new Customer() { customerName = "Sangam Sahai ", address = "#2 Tech Street, Lubbock TX", phone = "555-5555", email = "Sangam@email.com", customerIdStatus = "V" };
                    Customer customerJayson = new Customer() { customerName = "Jayson Brown", address = "#42 Prefect Lane, Vogon TX", phone = "259-555-1234", email = "JB@email.com", customerIdStatus = "V" };
                    context.Customers.Add(customerKen);
                    context.Customers.Add(customerSangam);
                    context.Customers.Add(customerJayson);
                    context.SaveChanges();

                    CheckingAccount checkingAccountKen = new CheckingAccount() { accountNumber = crypto.Encrypt("Ken-123C"), balance = 700, customerID = customerKen.customerID, isChecking = true, lastDepositAmount = 0 };
                    SavingsAccount savingsAccountKen = new SavingsAccount() { accountNumber = crypto.Encrypt("Ken-123S"), balance = 300, customerID = customerKen.customerID, isChecking = false, Interest = 100 };
                    context.CheckingAccounts.Add(checkingAccountKen);
                    context.SavingsAccounts.Add(savingsAccountKen);

                    TransferTransaction transferTransactionKen1 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("External"), toAccountNumber = crypto.Encrypt("Ken-123C"), amount = 1000, dateTime = DateTime.Parse("1 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    TransferTransaction transferTransactionKen2 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("Ken-123C"), toAccountNumber = crypto.Encrypt("Ken-123S"), amount = 100, dateTime = DateTime.Parse("5 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    TransferTransaction transferTransactionKen3 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("Ken-123C"), toAccountNumber = crypto.Encrypt("Ken-123S"), amount = 100, dateTime = DateTime.Parse("12 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    TransferTransaction transferTransactionKen4 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("Ken-123S"), toAccountNumber = crypto.Encrypt("Ken-123C"), amount = 100, dateTime = DateTime.Parse("19 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    TransferTransaction transferTransactionKen5 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("Ken-123C"), toAccountNumber = crypto.Encrypt("Ken-123S"), amount = 200, dateTime = DateTime.Parse("19 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    context.TransferTransactions.Add(transferTransactionKen1);
                    context.TransferTransactions.Add(transferTransactionKen2);
                    context.TransferTransactions.Add(transferTransactionKen3);
                    context.TransferTransactions.Add(transferTransactionKen4);
                    context.TransferTransactions.Add(transferTransactionKen5);

                    CheckingAccount checkingAccountSangam = new CheckingAccount() { accountNumber = crypto.Encrypt("Sangam-123C"), balance = 70, customerID = customerSangam.customerID, isChecking = true, lastDepositAmount = 0 };
                    SavingsAccount savingsAccountSangam = new SavingsAccount() { accountNumber = crypto.Encrypt("Sangam-123S"), balance = 30, customerID = customerSangam.customerID, isChecking = false, Interest = 5 };
                    context.CheckingAccounts.Add(checkingAccountSangam);
                    context.SavingsAccounts.Add(savingsAccountSangam);

                    TransferTransaction transferTransactionSangam1 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("External"), toAccountNumber = crypto.Encrypt("Sangam-123C"), amount = 100, dateTime = DateTime.Parse("2 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    TransferTransaction transferTransactionSangam2 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("Sangam-123C"), toAccountNumber = crypto.Encrypt("Sangam-123S"), amount = 10, dateTime = DateTime.Parse("3 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    TransferTransaction transferTransactionSangam3 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("Sangam-123C"), toAccountNumber = crypto.Encrypt("Sangam-123S"), amount = 10, dateTime = DateTime.Parse("7 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    TransferTransaction transferTransactionSangam4 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("Sangam-123S"), toAccountNumber = crypto.Encrypt("Sangam-123C"), amount = 10, dateTime = DateTime.Parse("12 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    TransferTransaction transferTransactionSangam5 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("Sangam-123C"), toAccountNumber = crypto.Encrypt("Sangam-123S"), amount = 20, dateTime = DateTime.Parse("15 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    context.TransferTransactions.Add(transferTransactionSangam1);
                    context.TransferTransactions.Add(transferTransactionSangam2);
                    context.TransferTransactions.Add(transferTransactionSangam3);
                    context.TransferTransactions.Add(transferTransactionSangam4);
                    context.TransferTransactions.Add(transferTransactionSangam5);

                    CheckingAccount checkingAccountJayson = new CheckingAccount() { accountNumber = crypto.Encrypt("Jayson-123C"), balance = 0, customerID = customerJayson.customerID, isChecking = true, lastDepositAmount = 0 };
                    SavingsAccount savingsAccountJayson = new SavingsAccount() { accountNumber = crypto.Encrypt("Jayson-123S"), balance = 0, customerID = customerJayson.customerID, isChecking = false, Interest = 1 };
                    context.CheckingAccounts.Add(checkingAccountJayson);
                    context.SavingsAccounts.Add(savingsAccountJayson);
                    context.SaveChanges();

                    TransferTransaction transferTransactionJayson1 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("External"), toAccountNumber = crypto.Encrypt("Jayson-123C"), amount = 10, dateTime = DateTime.Parse("3 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    TransferTransaction transferTransactionJayson2 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("Jayson-123C"), toAccountNumber = crypto.Encrypt("Jayson-123S"), amount = 1, dateTime = DateTime.Parse("4 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    TransferTransaction transferTransactionJayson3 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("Jayson-123C"), toAccountNumber = crypto.Encrypt("Jayson-123S"), amount = 1, dateTime = DateTime.Parse("6 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    TransferTransaction transferTransactionJayson4 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("Jayson-123S"), toAccountNumber = crypto.Encrypt("Jayson-123C"), amount = 1, dateTime = DateTime.Parse("14 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    TransferTransaction transferTransactionJayson5 = new TransferTransaction() { fromAccountNumber = crypto.Encrypt("Jayson-123C"), toAccountNumber = crypto.Encrypt("Jayson-123S"), amount = 2, dateTime = DateTime.Parse("17 Jan 2015"), transactionType = Transaction.TransactionType.Transfer, status = "Success" };
                    context.TransferTransactions.Add(transferTransactionJayson1);
                    context.TransferTransactions.Add(transferTransactionJayson2);
                    context.TransferTransactions.Add(transferTransactionJayson3);
                    context.TransferTransactions.Add(transferTransactionJayson4);
                    context.TransferTransactions.Add(transferTransactionJayson5);

                    Alert alertKen = new Alert() { accountNumber = checkingAccountKen.accountNumber, alertData = "Balance < 1,000,000" };
                    Alert alertSangam = new Alert() { accountNumber = checkingAccountSangam.accountNumber, alertData = "Balance < 1,000" };
                    Alert alertJayson = new Alert() { accountNumber = checkingAccountJayson.accountNumber, alertData = "Balance < 1" };
                    context.Alerts.Add(alertKen);
                    context.Alerts.Add(alertSangam);
                    context.Alerts.Add(alertJayson);

                    CheckImage checkImage100 = new CheckImage() { accountNumber = crypto.Encrypt("Ken-123C"), checkNumber = "1", checkImage = "\\img\\check100.jpg" };
                    CheckImage checkImage10 = new CheckImage() { accountNumber = crypto.Encrypt("Sangam-123C"), checkNumber = "1", checkImage = "\\img\\check10.jpg" };
                    CheckImage checkImage1 = new CheckImage() { accountNumber = crypto.Encrypt("Jayson-123C"), checkNumber = "1", checkImage = "\\img\\check1.jpg" };
                    context.CheckImages.Add(checkImage100);
                    context.CheckImages.Add(checkImage10);
                    context.CheckImages.Add(checkImage1);
                    context.SaveChanges();
                }
            }

        }
    }
}
