using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.DAL
{
    class EnumTransactionTypes
    {
        enum Transactiontypes{
            IDValidation, Query, Statement, Alert, Profile, UnlockID, Transfer
        }
    }
}
