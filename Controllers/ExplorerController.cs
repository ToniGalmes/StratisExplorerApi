using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StratisExplorerApi.Controllers
{
    [Route("api/explorer")]
    [ApiController]
    public class ExplorerController : ControllerBase
    {
        [HttpGet]
        [Route("block/hash/{hash}")]
        public ActionResult<object> BlockHash(string hash)
        {
            return "ok";
        }

        [HttpGet]
        [Route("address/{address}")]
        public ActionResult<object> Address(string address)
        {
            return "ok";
        }

        [HttpGet]
        [Route("transaction/{transactionId}")]
        public ActionResult<object> Transaction(string transactionId)
        {
            return "ok";
        }
    }
}