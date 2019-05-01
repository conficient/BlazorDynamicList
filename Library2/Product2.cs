using BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library2
{
    public class Product2 : ProductBase
    {
        /// <summary>
        /// number of grommets this has
        /// </summary>
        public int Grommets { get; set; }

        public override Type GetViewComponent()
        {
            return typeof(Component2);
        }

    }
}
