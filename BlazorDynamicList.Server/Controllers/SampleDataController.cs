using BaseClasses;
using BlazorDynamicList.Shared;
using Library1;
using Library2;
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
        private Random rng = new Random();


        [HttpGet("[action]")]
        public IEnumerable<ProductBase> Products()
        {
            return Enumerable.Range(1, 5).Select(index => CreateProduct(index));
        }

        private ProductBase CreateProduct(int index)
        {
            var tmp = rng.NextDouble();
            if (tmp < 0.6)
                return CreateProduct1(index);
            else
                return CreateProduct2(index);
        }

        private Product1 CreateProduct1(int index)
        {
            return new Product1()
            {
                ID = index + 1,
                Name = $"Example Product1 {index + 1}",
                Price = CreateRandomPrice(),
                Image = "https://loremflickr.com/100/75/product",
                HasFlange = rng.NextDouble() > 0.5f
                
            };
        }

        private Product2 CreateProduct2(int index)
        {
            return new Product2()
            {
                ID = index + 1,
                Name = $"Sample Product2 {index + 1}",
                Price = CreateRandomPrice(),
                Image = "https://loremflickr.com/100/75/product",
                Grommets = rng.Next(0,5)
            };
        }

        private decimal CreateRandomPrice()
        {
            const decimal range = 90m;
            const decimal basePrice = 10m;
            return Convert.ToDecimal(rng.Next(0, (int)range * 100)) / 100 + basePrice;
        }
    }
}
