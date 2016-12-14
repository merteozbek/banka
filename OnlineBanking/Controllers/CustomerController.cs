﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace OnlineBanking.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using OnlineBanking.DataModel;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// The CustomerInterface class manages the presentation layer for the customer.
    /// </summary>
    public class CustomerController : Controller
    {
        // these methods were in the initial design, but are implemented using the existing functionality in the ASP.Net technology.
        ///// <summary>
        ///// Allows the customer to log in to the system with a user ID and Password (PIN).
        ///// </summary>
        //public virtual void Login()
        //{
        //    throw new System.NotImplementedException();
        //}

        ///// <summary>
        ///// Displays the menu.
        ///// </summary>
        //private void DisplayMenu()
        //{
        //    throw new System.NotImplementedException();
        //}

        ///// <summary>
        ///// Displays error messages.
        ///// </summary>
        //private void DisplayError()
        //{
        //    throw new System.NotImplementedException();
        //}


        public ActionResult AccountStatement()
        {

            return View();
        }

        /// <summary>
        /// return the customers account stateent for the given month
        /// </summary>
        /// <param name="accountNumber">The acocunt number to get the statement for</param>
        /// <param name="month">the numeric month for the statement transactions</param>
        /// <returns>Redirect to DisplayAccountInfo</returns>
        [HttpPost]
        public ActionResult AccountStatement(string accountNumber, string month)
        {
            DateTime fromTime = DateTime.Now;
            DateTime toTime = DateTime.Now;
            try
            {
                fromTime = DateTime.Parse(String.Format("{0}/{1}/{2}", 1, month, DateTime.Now.Year));
                toTime = DateTime.Parse(String.Format("{0}/{1}/{2}", 1, (month + 1), DateTime.Now.Year));
            }
            catch (Exception)
            {
                // Do nothing on error.
            }
            return RedirectToAction("DisplayAccountInfo", new { accountNumber = accountNumber, fromTime = fromTime, toTime = toTime });
        }


        /// <summary>
        /// Redirects to the DisplayAccountInfo method so we can get the view and model.
        /// </summary>
        /// <param name="accountNumber">The account number to display</param>
        /// <param name="fromTime">The begining date for the transaction list</param>
        /// <param name="toTime">The ending date for the transaction list</param>
        /// <returns>redirect to DisplayAccountInfo view</returns>
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult CheckAccountInput(string accountNumber, Nullable<DateTime> fromTime = null, Nullable<DateTime> toTime = null)
        {
            return RedirectToAction("DisplayAccountInfo", new { accountNumber = accountNumber, fromTime = fromTime, toTime = toTime });
        }

        /// <summary>
        /// Returns the view that allows the user to enter the acocunt number and the begin and end dates for transactions
        /// </summary>
        /// <returns>view</returns>
        [Authorize(Roles = "Customer")]
        [HttpGet]
        public ActionResult CheckAccountInput()
        {
            return View();
        }

        /// <summary>
        ///If the money in any of the accounts of the person who is logged in , is
        ///less than the set up alert , then update the message as below, 
        ///otherwise update as 'Currently money in your accounts is above the alert ammount.'
        /// </summary>
        /// <returns>view</returns>
        [Authorize(Roles = "Customer")]
        public ActionResult CheckAlert()
        {
            ViewBag.checkAlertMessage = "Your money in account XYT123 is below the alert ammount !";
            return View();
        }

        /// <summary>
        /// Present the user with a view that will allow them to enter alert information.
        /// </summary>
        /// <returns>view</returns>
        [Authorize(Roles = "Customer")]
        public ActionResult SetUpAlert()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<ActionResult> SetUpAlert(string alertAmmount, string accountNumber)
        {
            bool success = false;
            try
            {
                OnlineBanking.BusinessProcess.ProfileManager profileManager = new OnlineBanking.BusinessProcess.ProfileManager();
                Customer customer = await profileManager.RequestCustomerProfile(User.Identity.GetUserName());
                if (customer != null)
                {
                    Alert alert = new Alert() { accountNumber = accountNumber, alertData = alertAmmount };
                    OnlineBanking.BusinessProcess.AlertManager alertManager = new BusinessProcess.AlertManager();
                    success = await alertManager.SetUpAccountAlerts(alert, customer.customerID);
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception)
            {
                success = false;
            }
            if (success)
            {
                ViewBag.setUpAlertMessage = string.Format("The alert for ${0} was set up for account {1}", alertAmmount, accountNumber);
            }
            else
            {
                ViewBag.setUpAlertMessage = string.Format("There was a problem setting up the alert for ${0} for account {1}", alertAmmount, accountNumber);
            }
            return View();
        }

        [Authorize(Roles = "Customer")]
        public ActionResult CheckBalance()
        {
            return View();
        }

        /// <summary>
        /// Calculate the balance in the account - account , and update the balanceMessage property accordingy !!
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult CheckBalance(String account)
        {
            OnlineBanking.BusinessProcess.QueryManager queryManager = new OnlineBanking.BusinessProcess.QueryManager();
            //queryManager.CheckAccount()
            ViewBag.balanceMessage = "Your balance is 123 $";
            return View();
        }


        /// <summary>
        /// Displays the account balance and transactions between the given dates
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<ActionResult> DisplayAccountInfo(string accountNumber, Nullable<DateTime> fromTime = null, Nullable<DateTime> toTime = null)
        {
            List<Models.TransactionModel> model = new List<Models.TransactionModel>();
            try
            {
                DateTime _fromTime;
                DateTime _toTime;
                if (fromTime == null)
                {
                    fromTime = DateTime.Now;
                }
                if (toTime == null)
                {
                    toTime = DateTime.Now;
                }
                _fromTime = fromTime.Value;
                _toTime = toTime.Value;

                OnlineBanking.BusinessProcess.ProfileManager profileManager = new OnlineBanking.BusinessProcess.ProfileManager();
                Customer customer = await profileManager.RequestCustomerProfile(User.Identity.GetUserName());
                if (customer != null)
                {
                    OnlineBanking.BusinessProcess.QueryManager querymanager = new BusinessProcess.QueryManager();
                    Account account = await querymanager.CheckAccount(accountNumber, _fromTime, _toTime, customer.customerID);
                    if (account.Statement != null)
                    {
                        foreach (DataModel.TransferTransaction transaction in account.Statement)
                        {
                            model.Add(new Models.TransactionModel()
                            {
                                amount = transaction.amount,
                                dateTime = transaction.dateTime,
                                fromAccountNumber = transaction.fromAccountNumber,
                                toAccountNumber = transaction.toAccountNumber
                            });
                        }
                        ViewBag.accounNumberDetails = "Account Number -" + account.accountNumber;
                        ViewBag.balanceDetails = "Account Balance - $" + account.balance;
                    }
                }
                else
                {
                    ViewBag.accounNumberDetails = "Account Nont found";
                    ViewBag.balanceDetails = "Account Balance";
                }

            }
            catch (Exception)
            {
                ViewBag.ErrorDetails = "There was a problem processing your request.";
            }
            return View(model);
        }


        /// <summary>
        /// Allows the customer to obtain an image of the check for a given transation.
        /// </summary>
        [Authorize(Roles = "Customer")]
        public virtual void CheckImageRequest()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Displays the image of a check.
        /// </summary>
        [Authorize(Roles = "Customer")]
        private void DisplayCheckImage()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// allows the customer to request a statement on their account for a given month.
        /// </summary>
        [Authorize(Roles = "Customer")]
        public virtual void AccountStatementRequest()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Allows the customer to enter alerts data to create alerts.
        /// </summary>
        [Authorize(Roles = "Customer")]
        public virtual void AccountAlertsInput()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Displays the status of the last request.
        /// </summary>
        [Authorize(Roles = "Customer")]
        private void DisplayStatus()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Allows the customer to see their profile information.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> CustomerProfileInput(string name, string phone, string address)
        {
            bool success = false;
            try
            {
                OnlineBanking.BusinessProcess.ProfileManager profileManager = new BusinessProcess.ProfileManager();
                Customer customer = await profileManager.RequestCustomerProfile(User.Identity.GetUserName());
                if (customer != null)
                {
                    customer.address = address;
                    customer.phone = phone;
                    customer.customerName = name;
                    success = await profileManager.UpdateCustomerProfile(customer);
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception)
            {
                success = false;
            }
            if (success)
            {
                ViewBag.StatusMessage = String.Format("Customer profile updated. Name:{0}, Phone:{1}, Address:{2}", name, phone, address);
            }
            else
            {
                ViewBag.StatusMessage = String.Format("Customer profile NOT updated. Name:{0}, Phone:{1}, Address:{2}", name, phone, address);
            }
            return View();
        }

        /// <summary>
        /// Allows the customer to see their profile information.
        /// </summary>
        [Authorize(Roles = "Customer")]
        public ActionResult CustomerProfileInput()
        {
            return View();
        }


        [Authorize(Roles = "Customer")]
        public ActionResult TransferFunds()
        {

            return View();
        }

        /// <summary>
        ///Debit the ammount from the fromAccount and credit it to toAccount
        ///And update the transferFundSuccess as 'success' on a successfull transaction
        ///and update it as 'failure' in case of a failed transaction.
        /// </summary>
        /// <param name="fromAccount"></param>
        /// <param name="toAccount"></param>
        /// <param name="ammount"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<ActionResult> TransferFunds(String fromAccount, String toAccount, String ammount)
        {
            bool success = false;
            try
            {
                double Amount = double.Parse(ammount);
                OnlineBanking.BusinessProcess.ProfileManager profileManager = new BusinessProcess.ProfileManager();
                Customer customer = await profileManager.RequestCustomerProfile(User.Identity.GetUserName());
                if (customer != null)
                {

                    OnlineBanking.BusinessProcess.TransferManager transferManager = new BusinessProcess.TransferManager();
                    success = await transferManager.Transfer(fromAccount, toAccount, Amount, customer.customerID);
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception)
            {
                success = false;
            }
            if (success)
            {
                ViewBag.transferFundSuccess = "The funds transfered successfully.";
            }
            else
            {
                ViewBag.transferFundSuccess = "There was a problem transfering the funds.";
            }
            return View();
        }
    }
}

