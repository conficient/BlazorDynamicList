using BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library1
{
    public class Product1 : ProductBase
    {
        /// <summary>
        /// Product 1 specific properties
        /// </summary>
        public bool HasFlange { get; set; }

        /// <summary>
        /// Tell the consumer which component to use to view
        /// </summary>
        /// <returns></returns>
        public override Type GetViewComponent()
        {
            if (HasFlange)
                return typeof(Component1b);
            else
                return typeof(Component1);
        }

    }
}
