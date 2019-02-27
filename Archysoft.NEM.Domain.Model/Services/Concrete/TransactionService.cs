using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Archysoft.NEM.Data.Repositories.Abstract;
using Archysoft.NEM.Domain.Model.Services.Abstract;
using io.nem1.sdk.Infrastructure.HttpRepositories;
using io.nem1.sdk.Model.Accounts;
using io.nem1.sdk.Model.Blockchain;
using io.nem1.sdk.Model.Mosaics;
using io.nem1.sdk.Model.Transactions;
using io.nem1.sdk.Model.Transactions.Messages;
using TransactionInfo = Archysoft.NEM.Data.Entities.TransactionInfo;


namespace Archysoft.NEM.Domain.Model.Services.Concrete
{
    public class TransactionService : ITransactionService
    {

        private ITransactionInfoRepository _transactionInfoRepository;

        public TransactionService(ITransactionInfoRepository transactionInfoRepository)
        {
            _transactionInfoRepository = transactionInfoRepository;
        }


        string host = "http://23.228.67.85:7890";
        string host2 = "http://104.128.226.60:7890";

        private readonly string privateKey = "6ea3fd5f2cf4fbeb54cd96a48d11cd2ff0b4106472c6a97c7e4e5736243cb2db";
        private readonly string recipientAddress = "TACOPE-XRLZTU-WBQA3U-XV66R4-55L76E-NWK6OY-ITBJ";

        public async Task<TransactionResponse> Send(object data)
        {
            //var cosignatory = KeyPair.CreateFromPrivateKey("8db858dcc8e2827074498204b3829154ec4c4f24d13738d3f501003b518ef256");
            //var secondCosig = KeyPair.CreateFromPrivateKey("cfe47dd9801a5d4fe37183e8f6ca49fff532a2fe6fe099436df93b3d62fe17d5");
            //var multisigAccount = PublicAccount.CreateFromPublicKey("29c4a4aa674953749053c8a35399b37b713dedd5d002cb29b3331e56ff1ea65a", NetworkType.Types.TEST_NET);
            //var recipient = new Account("E45030D2A22D97FDC4C78923C4BBF7602BBAC3B018FFAD2ED278FB49CD6F218C", NetworkType.Types.TEST_NET);
        

            KeyPair keyPair = KeyPair.CreateFromPrivateKey(privateKey);

            TransferTransaction transaction = TransferTransaction.Create(
                NetworkType.Types.TEST_NET,
                Deadline.CreateHours(1),
                Address.CreateFromEncoded(recipientAddress),
                new List<Mosaic> { Mosaic.CreateFromIdentifier("nem:xem", 1) },
                PlainMessage.Create(data.ToString())
            );

            SignedTransaction signedTransaction = transaction.SignWith(keyPair);
          
            TransactionResponse response = await new TransactionHttp(host).Announce(signedTransaction);



            if (response.Status == "1")
            {
                TransactionInfo transactionInfo = new TransactionInfo
                { Id = 1, Hash = response.Hash, InnerHash = response.InnerHash };
                _transactionInfoRepository.AddTransaction(transactionInfo);
            }

            return null;
        }
    }
}
