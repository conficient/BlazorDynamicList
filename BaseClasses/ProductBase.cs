using System;
using System.Linq;

namespace BaseClasses
{
    /// <summary>
    /// A base class for all product instances
    /// </summary>
    public abstract class ProductBase
    {
        /// <summary>
        /// An ID 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Image URL
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Abstract method to get the view component type to use
        /// </summary>
        /// <returns></returns>
        public abstract Type GetViewComponent();


    }
}
