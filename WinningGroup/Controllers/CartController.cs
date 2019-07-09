using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using WinningGroup.Repository;
using WinningGroup.Utility;

namespace WinningGroup.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private IProductRepository _repository;
        private IMemoryCache _cache;
        private IConfiguration _configuration;
        private MemoryCacheEntryOptions _cacheEntryOptions;

        public static readonly string CART_CacheKey = "CART";

        public CartController(IProductRepository repository, IMemoryCache cache, IConfiguration configuration)
        {
            _repository = repository;
            _cache = cache;
            _configuration = configuration;

            _cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSize(Convert.ToInt32(_configuration["CachingConfigurations:CachedSize"]))
                .SetSlidingExpiration(TimeSpan.FromMinutes(Convert.ToInt32(_configuration["CachingConfigurations:CachedMinutes"])));
        }

        [HttpGet()]
        public List<Models.Product> GetCart()
        {
            return GetCartContent();
        }

        [HttpPost("add/{id}")]
        public ActionResult<CommonJsonDataModel> AddProduct(int id)
        {
            var cacheEntry = GetCartContent();

            try
            {
                var toAdd = _repository.GetProductById(id);

                if (toAdd != null)
                {
                    cacheEntry.Add(toAdd);
                    _cache.Set(CART_CacheKey, cacheEntry, _cacheEntryOptions);
                }

                return CommonJsonDataFactory.Create(JsonResponseType.Ok, cacheEntry);
            }
            catch(Exception ex)
            {
                return CommonJsonDataFactory.Create(JsonResponseType.BadRequest
                  , errors: ex.Message);
            }
        }

        [HttpPost("remove/{id}")]
        public ActionResult<CommonJsonDataModel> RemoveProduct(int id)
        {
            var cacheEntry = GetCartContent();

            if(cacheEntry.Count == 0)
            {
                return CommonJsonDataFactory.Create(JsonResponseType.Ok, cacheEntry);
            }

            try
            {
                var toRemove = cacheEntry.First(r => r.ID == id);
                if (toRemove != null)
                {
                    cacheEntry.Remove(toRemove);
                    _cache.Set(CART_CacheKey, cacheEntry, _cacheEntryOptions);
                }

                return CommonJsonDataFactory.Create(JsonResponseType.Ok, cacheEntry);
            }
            catch (Exception ex)
            {
                return CommonJsonDataFactory.Create(JsonResponseType.BadRequest
                  , errors: ex.Message);
            }
        }

        private List<Models.Product> GetCartContent()
        {
            var hasCachedCART = _cache.TryGetValue(CART_CacheKey, out List<Models.Product> cacheEntry);

            if (!hasCachedCART)
            {
                cacheEntry = new List<Models.Product>();
            }

            return cacheEntry;
        }
    }
}