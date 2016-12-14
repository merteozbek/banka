﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace OnlineBanking.DataModel
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents an abstract bank account.
    /// </summary>
	public abstract class Account
	{
 
        /// <summary>
        /// The unique identifier of the account.
        /// </summary>
        [Key]
        [MaxLength(25)]
        public string accountNumber
		{
			get;
			set;
		}

        /// <summary>
        /// True if this is a checking account.
        /// </summary>
        public bool isChecking
        {
            get;
            set;
        }

        /// <summary>
        /// The current account balance.
        /// </summary>
		public Double balance
		{
			get;
			set;
		}

        /// <summary>
        /// The customer who owns the account. 
        /// Note that the data model is inconsistent here. In the conceptual static model, it looks like many customers may each own many accounts.
        /// In the entity models, the account is owned by a single customer. Going with the single customer model.
        /// </summary>
        public int customerID
        {
            get;
            set;
        }

        /// <summary>
        /// The active Alert on the account if applicable. The design includes only a single active alert.
        /// </summary>
		public virtual Alert Alert
		{
			get;
			set;
		}

        /// <summary>
        /// The collection of transactions for the account.
        /// </summary>
        public virtual IEnumerable<TransferTransaction> Statement
        {
            get;
            set;
        }

        
        ///// <summary>
        ///// Read an set of account numbers from the data store based on the users ID.
        ///// </summary>
        ///// <param name="ID">The ID of the user that is associated with the accounts</param>
        ///// <returns>Account data</returns>
        //public virtual string Read(string ID)
        //{
        //    throw new System.NotImplementedException();
        //}

        ///// <summary>
        ///// Read an Account balance from the data store.
        ///// </summary>
        ///// <param name="accountNumber">The account number to read the balance for</param>
        ///// <returns>the current account balance.</returns>
        //public virtual Double ReadBalance(string accountNumber)
        //{
        //    throw new System.NotImplementedException();
        //}

        /// <summary>
        /// Create a debit entry on the account and modify the balance accordingly.
        /// </summary>
        /// <param name="amount">The amount to debit.</param>
        /// <returns></returns>
        public virtual bool Debit(Double amount)
        {
            if (this.balance >= amount)
            {
                this.balance = this.balance - amount;
            }
            else
            {
                throw new Exception(String.Format("Insufficient funds in account {0}", this.accountNumber));
            }
            return true;
        }

        /// <summary>
        /// Create a credit entry on the account and modify the balance accordingly.
        /// </summary>
        /// <param name="amount">The amount to credit.</param>
        /// <returns></returns>
        public virtual bool Credit(Double amount)
        {
            this.balance = this.balance + amount;
            return true;
        }
	}
}

