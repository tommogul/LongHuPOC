using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace LongHu.BusinessLogic.Utility
{
    public static class TransactionHelper<T>
    {
        private static IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        private static TransactionScopeOption transactionScopeOption = TransactionScopeOption.RequiresNew;

        public static T Execute(TransactionDelegte<T> dele)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = isolationLevel;
            using (var transactionScope = new TransactionScope(transactionScopeOption, option))
            {
                T result = dele();
                transactionScope.Complete();
                return result;
            }
        }
    }

    public delegate T TransactionDelegte<T>();

   
}

