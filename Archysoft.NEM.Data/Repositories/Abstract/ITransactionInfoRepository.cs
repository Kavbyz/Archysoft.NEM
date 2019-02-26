
using System.Collections.Generic;
using Archysoft.NEM.Data.Entities;


namespace Archysoft.NEM.Data.Repositories.Abstract
{
    public interface ITransactionInfoRepository
    {
        List<TransactionInfo> Get();

        TransactionInfo GetTransaction(ulong id);

        void AddTransaction(TransactionInfo transaction);

    }
}
