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
    /// Customerr unlock transaction data for logging.
    /// </summary>
	public class UnlockIDTransaction : Transaction
	{
        /// <summary>
        /// The account number associated with the customer. Shouldn't this be the Customer ID?
        /// </summary>
        [MaxLength(25)]
        public string accountNumber
		{
			get;
			set;
		}

        /// <summary>
        /// The ID of the operator who unlocked the customer profile.
        /// </summary>
        [MaxLength(25)]
        public string operatorId
		{
			get;
			set;
		}

	}
}
