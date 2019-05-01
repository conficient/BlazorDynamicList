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
        private ProductBase Product { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            builder.OpenElement(0, "p");
            builder.AddContent(1, Product?.Name);
            builder.CloseElement();
        }
    }
}
