using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using OnlineBanking.DataModel;


namespace OnlineBanking.DAL
{
    public class BankContext : DbContext
    {

        public BankContext()  : base("name=DefaultConnection")
        {
        }

        public DbSet<Alert> Alerts { get; set; }
        public DbSet<AlertTransaction> AlertTransactions { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<CheckImage> CheckImages { get; set; }
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<IDValidationTransaction> IDValidationTransactions { get; set; }
        public DbSet<ProfileTransaction> ProfileTransactions { get; set; }
        public DbSet<QueryTransaction> QueryTransactions { get; set; }
        public DbSet<SavingsAccount> SavingsAccounts { get; set; }
        //public DbSet<Statement> Statements { get; set; }
        public DbSet<StatementTransaction> StatementTransactions { get; set; }
        public DbSet<TransferTransaction> TransferTransactions { get; set; }
        public DbSet<UnlockIDTransaction> UnlockIDTransactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
