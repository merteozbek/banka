using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBanking.Models
{
    public class TransactionModel
    {
        public string fromAccountNumber { get; set; }
        public string toAccountNumber { get; set; }

        public double amount { get; set; }

        public DateTime dateTime { get; set; }
    }
}