using System;

namespace BaseClasses
{
    public abstract class ProductBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public abstract Type GetViewComponent();
        public abstract string GetViewComponentName();
    }
}
