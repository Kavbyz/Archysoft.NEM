using Archysoft.NEM.Domain.Model.Services.Abstract;
using Archysoft.NEM.Web.Api.Routes;
using Microsoft.AspNetCore.Mvc;

namespace Archysoft.NEM.Web.Api.Controllers
{
    public class TransactionController
    {
        private readonly ITransactionService _transactionService;

        string body2 =
            "{\r\n  type: 4098,\r\n  version: -1744830463,\r\n  signer: '0257b05f601ff829fdff84956fb5e3c65470a62375a1cc285779edd5ca3b42f6',\r\n  timeStamp: 62995509,\r\n  deadline: 62999109,\r\n  otherHash: {\r\n    data: '161d7f74ab9d332acd46f96650e74371d65b6e1a0f47b076bdd7ccea37903175'\r\n  },\r\n  otherAccount: 'TBCI2A67UQZAKCR6NS4JWAEICEIGEIM72G3MVW5S',\r\n  fee: 6000000\r\n}";


        string body =
            "{{\r\n  \"data\": \"0410000001000098e96e5d07200000009d7ea57169a56a1bb821e1abf744610c639d7545f976f09808b68a6ad1415eb0f049020000000000f97c5d07a30000000101000002000098e96e5d072000000029c4a4aa674953749053c8a35399b37b713dedd5d002cb29b3331e56ff1ea65aa086010000000000098b5d07280000005444414b46324d42494c4f4c335a433434544650434254463744444841464b5648464653504d325640420f00000000000d000000010000000500000068656c6c6f010000001a0000000e000000030000006e656d0300000078656d40420f0000000000\",\r\n  \"signature\": \"c6c6ab8e81b0dcf8b1c11cb051f7f2900c12eaad2cb1196b428732961b4948ac8db0b384022293830c2796a516d3e9760e0252f996ba1428f04e427bfcecfa07\"\r\n}}";


        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [Route(RoutePaths.SendTransaction)]
        public ActionResult<bool> SendTransaction()
        {
            _transactionService.Send(body);
            return true;
        }
    }
}
