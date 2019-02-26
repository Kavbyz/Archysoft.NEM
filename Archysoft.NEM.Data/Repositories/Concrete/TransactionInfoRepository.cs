

using System.Collections.Generic;
using System.Linq;
using Archysoft.NEM.Data.Entities;
using Archysoft.NEM.Data.Repositories.Abstract;

namespace Archysoft.NEM.Data.Repositories.Concrete
{
    public class TransactionInfoRepository:ITransactionInfoRepository
    {
        private List<TransactionInfo> Transactions = new List<TransactionInfo>();

        public List<TransactionInfo> Get()
        {
            return Transactions;
        }

        public TransactionInfo GetTransaction(ulong id)
        {
            return Transactions.Single(transaction => transaction.Id == id);
        }

        public void AddTransaction(TransactionInfo transaction)
        {
            Transactions.Add(transaction);
        }
    }
}
