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

        public override Type GetViewComponent()
        {
            return typeof(Component1);
        }

        public override string GetViewComponentName()
        {
            return typeof(Component1).FullName;
        }
    }
}
