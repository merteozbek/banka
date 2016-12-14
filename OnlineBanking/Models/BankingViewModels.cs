using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Models
{
    public class BankAccountViewModel
    {
        [MaxLength(25)]
        string accounttype { get; set; }
        [MaxLength(25)]
        string accountNumber { get; set; }
        double balance { get; set; }
        double lastDeposit { get; set; }
        double interestrate { get; set; }

    public BankAccountViewModel (OnlineBanking.DataModel.Account account)
    {
        if (account != null)
        {
            if (account.isChecking)
            {
                this.accounttype = "Checking";
            }
            else
            {
                this.accounttype = "Savings";
            }
            this.accountNumber = account.accountNumber;
            this.balance = account.balance;

        }
        else
        {
            this.accounttype = "Undefined";
            this.accountNumber = string.Empty;
            this.balance = 0;
            this.interestrate = 0;
            this.lastDeposit = 0;
        }
    }
    }
}