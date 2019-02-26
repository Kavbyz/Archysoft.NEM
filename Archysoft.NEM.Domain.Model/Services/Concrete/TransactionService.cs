using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Archysoft.NEM.Domain.Model.Services.Abstract;
using io.nem1.sdk.Infrastructure.HttpRepositories;
using io.nem1.sdk.Model.Accounts;
using io.nem1.sdk.Model.Blockchain;
using io.nem1.sdk.Model.Mosaics;
using io.nem1.sdk.Model.Transactions;
using io.nem1.sdk.Model.Transactions.Messages;
using RestSharp;
using SimpleJson;

namespace Archysoft.NEM.Domain.Model.Services.Concrete
{
    public class TransactionService:ITransactionService
    {
        string host = "http://23.228.67.85:7890";
        string host2 = "http://104.128.226.60:7890";
        public async Task<TransactionResponse> Send(object data)
        {
            var cosignatory = KeyPair.CreateFromPrivateKey("8db858dcc8e2827074498204b3829154ec4c4f24d13738d3f501003b518ef256");
            var secondCosig = KeyPair.CreateFromPrivateKey("cfe47dd9801a5d4fe37183e8f6ca49fff532a2fe6fe099436df93b3d62fe17d5");
            var multisigAccount = PublicAccount.CreateFromPublicKey("29c4a4aa674953749053c8a35399b37b713dedd5d002cb29b3331e56ff1ea65a", NetworkType.Types.TEST_NET);
            var recipient = new Account("E45030D2A22D97FDC4C78923C4BBF7602BBAC3B018FFAD2ED278FB49CD6F218C", NetworkType.Types.TEST_NET);

            var transaction = TransferTransaction.Create(
                NetworkType.Types.TEST_NET,
                Deadline.CreateHours(2),
                recipient.Address,
                new List<Mosaic> { Mosaic.CreateFromIdentifier("nem:xem", 1000000) },
                PlainMessage.Create("hello")
            );

            var multisigTransaction = MultisigTransaction
                .Create(NetworkType.Types.TEST_NET, Deadline.CreateHours(1), transaction)
                .SignWith(cosignatory, multisigAccount);



            var response = await new TransactionHttp(host).Announce(multisigTransaction);

            var signatureTransaction = CosignatureTransaction.Create(
                NetworkType.Types.TEST_NET,
                Deadline.CreateHours(1),
                "59f5f7cbbdaa996b8d3c45ce814280aab3b5d322a98fe95c00ae516cf436172d",
                multisigAccount.Address
            ).SignWith(secondCosig);

            var response2 = await new TransactionHttp(host).Announce(signatureTransaction);

            //var localVarPath = "/transaction/announce";
            //var client = new RestClient(host);
            //var request = new RestRequest(localVarPath, Method.POST);
            //request.AddParameter("application/json", data, ParameterType.RequestBody);
            //var resp = client.Execute(request);

            return null;
        }
    }
}
