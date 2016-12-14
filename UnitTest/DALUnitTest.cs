using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineBanking.DAL;
using OnlineBanking.DataModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace UnitTest
{
    /// <summary>
    /// Test cases for the Web API
    /// </summary>
    [TestClass]
    public class DALUnitTest
    {
        /// <summary>
        /// Test the AppUsersController Get method with parameters
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task TestGetBank()
        {
            // Setup
            BankContext bankContext = new BankContext();
            string bankName = "KSJ Bank";
            string bankAddress = "1 Bank Street, New York, New York";
            Bank storedBank = null;

            using (var context = new BankContext())
            {
                storedBank = await (context.Banks.Where(b => b.bankName == bankName).FirstOrDefaultAsync<Bank>());
            }

            // Validate
            Assert.IsNotNull(storedBank);
            Assert.IsTrue(storedBank.bankName == bankName);
            Assert.IsTrue(storedBank.bankAddress == bankAddress);
            
        }
    }
}