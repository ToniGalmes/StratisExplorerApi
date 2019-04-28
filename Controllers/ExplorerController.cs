using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace StratisExplorerApi.Controllers
{
    [Route("api/explorer")]
    [ApiController]
    public class ExplorerController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public ExplorerController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        
        [HttpGet]
        [Route("block/hash/{hash}")]
        public async Task<ActionResult<object>> BlockHashAsync(string hash)
        {
            string lastResponse;

            if (!_memoryCache.TryGetValue($"hash[{hash}]", out lastResponse))
            {
                string responseHeight;
                var client = new RestClient($"https://stratis.guru/api/block-height");
                var request = new RestRequest(Method.GET);
                responseHeight = (await client.ExecuteTaskAsync(request)).Content;

                if(Convert.ToInt32(hash) <= Convert.ToInt32(responseHeight))
                {
                    client = new RestClient($"https://stratis.guru/api/block/{hash}");
                    request = new RestRequest(Method.GET);
                    lastResponse = (await client.ExecuteTaskAsync(request)).Content;
                
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1));
                    _memoryCache.Set($"hash[{hash}]", lastResponse, cacheEntryOptions);
                }else lastResponse = "Error, blockIdx not existent";
                
            }
            return lastResponse;
        }

        [HttpGet]
        [Route("address/{address}")]
        public async Task<ActionResult<object>> AddressAsync(string address)
        {
            string lastResponse;

            if (!_memoryCache.TryGetValue($"address[{address}]", out lastResponse))
            {
                var client = new RestClient($"https://stratis.guru/api/address/{address}");
                var request = new RestRequest(Method.GET);
                lastResponse = (await client.ExecuteTaskAsync(request)).Content;
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1));
                _memoryCache.Set($"address[{address}]", lastResponse, cacheEntryOptions);
            }

            return lastResponse;
        }

        [HttpGet]
        [Route("transaction/{transactionId}")]
        public async Task<ActionResult<object>> TransactionAsync(string transactionId)
        {
            string lastResponse;

            if (!_memoryCache.TryGetValue($"transaction[{transactionId}]", out lastResponse))
            {
                var client = new RestClient($"https://stratis.guru/api/transaction/{transactionId}");
                var request = new RestRequest(Method.GET);
                lastResponse = (await client.ExecuteTaskAsync(request)).Content;
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1));
                _memoryCache.Set($"transaction[{transactionId}]", lastResponse, cacheEntryOptions);
            }

            return lastResponse;
        }
    }
}