using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineBanking.BusinessProcess;
using OnlineBanking.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.DAL;


namespace UnitTest
{

    [TestClass]
    public class BusinessProcessUnitTest
    {
        string accountNumber = "Ken-123C";
        string alertData = "Account balanace < 0";

        string customerToSaveAddress = "1600 Pennsylvania Ave, Washington DC";
        string customerToSaveName = "Mr. President";
        string customerToSaveEmail = "Pres@Whitehouse.gov";
        string customerToSavePassword = "Unencrypted";
        string customerToSavePhone = "555-1212";

        DateTime fromDate = DateTime.Parse("1 Jan 2015");
        DateTime toDate = DateTime.Parse("15 May 2015");

        string checkNumber = "1";

        /// <summary>
        /// Tests the SetupAlerts method
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task TestAlertManagerSetupAlerts()
        {
            //Setup
            AlertManager alertManager = new AlertManager();
            Alert alert = new Alert() {accountNumber = accountNumber, alertData = alertData };
            //Execute
            if (await alertManager.SetUpAccountAlerts(alert, 1) != true)
            {
                Assert.Fail("SetupAccountAlerts failed.");
            }
            //Validate
            IEnumerable<Alert> alerts = alertManager.GetAccountAlerts(accountNumber, 1);
            Assert.IsTrue(alerts.ToList<Alert>().Count > 0);
        }

        /// <summary>
        /// Tests the RequestCustomerProfile and UpdateCustomerProfile methods
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task TestProfileManagerRequestCustomerProfile()
        {
            //Setup
            ProfileManager profileManager = new ProfileManager();
            Customer customerToSave = new Customer() {
                address = customerToSaveAddress,
                customerIdStatus = "E" ,
                customerName = customerToSaveName,
                email = customerToSaveEmail,
                password = customerToSavePassword,
                phone = customerToSavePhone

            };
            //Execute
            if (await profileManager.UpdateCustomerProfile(customerToSave))
            {
                Assert.Fail("ProfileManager.RequestCustomerProfile failed.");
            }

            Customer customer = await profileManager.RequestCustomerProfile (customerToSave.email);
            //Validate
            Assert.IsTrue(customer.customerName == customerToSave.customerName);
        }

        /// <summary>
        /// Tests the QueryManager CheckAccount method
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task TestQueryManagerCheckAccount()
        {
            //Setup
            QueryManager querymanager = new QueryManager();
            //Execute
            Account account = await querymanager.CheckAccount(accountNumber, fromDate, toDate, 1);
            //Validate
            Assert.IsTrue(account.accountNumber == accountNumber);
            //Assert.IsTrue(account.Statement.ToList().Count > 0);
        }

        /// <summary>
        /// Tests the Query manager RequestCheckImage method
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task TestQueryManagerRequestCheckImage()
        {
            //Setup
            QueryManager queryManager = new QueryManager();
            //Execute
            CheckImage checkImage = await queryManager.RequestCheckImage(accountNumber, checkNumber);
            //Validate
            Assert.IsTrue(checkImage.accountNumber == accountNumber);
            Assert.IsTrue(checkImage.checkNumber == checkNumber);
        }

        /// <summary>
        /// Tests the RequestAccountStatement method
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task TestStatementManagerRequestAccountStatement()
        {
            //Setup
            //Execute
            //Validate
        }

        /// <summary>
        /// Tests the Transfer method
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task TestTransferManagerTransfer()
        {
            //Setup
            //Execute
            //Validate
        }

        /// <summary>
        /// Tests the LogFaildTransaction method
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task TestTransferManagerLogFailedTransaction()
        {
            //Setup
            //Execute
            //Validate
        }

        /// <summary>
        /// Tests the UnlockCustomerID method
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task TestUnlockIDManagerUnlockCustomerID()
        {
            //Setup
            //Execute
            //Validate
        }
    }
}
