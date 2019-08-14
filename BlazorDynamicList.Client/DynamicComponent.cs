using BaseClasses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDynamicList.Client
{
    public class DynamicComponent : ComponentBase
    {
        public DynamicComponent()
        {
        }
        
        
        [Parameter]
        public ProductBase Product { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            Type componentType = Product.GetViewComponent();
            builder.OpenComponent(0, componentType);
            builder.AddAttribute(1, "Product", Product);
            builder.CloseComponent();
        }
    }
}
