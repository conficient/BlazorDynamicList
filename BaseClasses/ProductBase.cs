using System;
using System.Linq;

namespace BaseClasses
{
    public abstract class ProductBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string Image { get; set; }

        public abstract Type GetViewComponent();


    }
}
