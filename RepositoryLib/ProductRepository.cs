using BaseClasses;
using Library1;
using Library2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLib
{
    public class ProductRepository
    {
        private readonly Random rng = new Random();

        public ProductBase[] GetProducts(int count)
        {
            return (Enumerable.Range(1, count)
                .Select(index => CreateRandomProduct(index))).ToArray();
        }

        /// <summary>
        /// Create either a Product1 or Product2 randomly
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private ProductBase CreateRandomProduct(int index)
        {
            var tmp = rng.NextDouble();
            if (tmp < 0.6)
                return CreateProduct1(index);
            else
                return CreateProduct2(index);
        }

        /// <summary>
        /// Create a Product1 instance
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Product1 CreateProduct1(int index)
        {
            return new Product1()
            {
                ID = index,
                Name = $"Example Product1 {index}",
                Price = CreateRandomPrice(),
                Image = RandomImage("Product1"),
                HasFlange = rng.NextDouble() > 0.5f

            };
        }

        /// <summary>
        /// Create a Product2 instance
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Product2 CreateProduct2(int index)
        {
            return new Product2()
            {
                ID = index,
                Name = $"Sample Product2 {index}",
                Price = CreateRandomPrice(),
                Image = RandomImage("Product2"),
                Grommets = rng.Next(0, 5)
            };
        }

        /// <summary>
        /// Generate a random price in the range $10-$100
        /// </summary>
        /// <returns></returns>
        private decimal CreateRandomPrice()
        {
            const decimal range = 90m;
            const decimal basePrice = 10m;
            return Convert.ToDecimal(rng.Next(0, (int)range * 100)) / 100 + basePrice;
        }

        private string RandomImage(string name)
        {
            var r = rng.Next(200, 255);
            var g = rng.Next(150, 255);
            var b = rng.Next(150, 255);
            string colour = $"{r:x}{g:x}{b:x}";
            return $"https://dummyimage.com/64x64/{colour}/000.png&text={name}";
        }
    }
}
