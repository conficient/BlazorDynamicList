using BaseClasses;
using RepositoryLib;
using BlazorDynamicList.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDynamicList.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        // create repository
        private readonly ProductRepository repo = new ProductRepository();

        /// <summary>
        /// get a list of products of different types
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public string Products(int count)
        {
            var productList = repo.GetProducts(count);
            return TypedSerializer.Serialize(productList);
        }

    }
}
