#pragma checksum "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Item\Parts\_Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1e5a0a9e1eb3d86fe2a82b0d1d3a8f33dc4f3e24"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Item_Parts__Details), @"mvc.1.0.view", @"/Views/Item/Parts/_Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Item/Parts/_Details.cshtml", typeof(AspNetCore.Views_Item_Parts__Details))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\_ViewImports.cshtml"
using PathfinderTracker;

#line default
#line hidden
#line 2 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\_ViewImports.cshtml"
using PathfinderTracker.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1e5a0a9e1eb3d86fe2a82b0d1d3a8f33dc4f3e24", @"/Views/Item/Parts/_Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"147e504006a9f7463f793a5e88f3c13df5b5652d", @"/Views/_ViewImports.cshtml")]
    public class Views_Item_Parts__Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Item>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(13, 55, true);
            WriteLiteral("\r\n<dl class=\"row\">\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(69, 40, false);
#line 5 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Item\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(109, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(159, 36, false);
#line 8 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Item\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(195, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(244, 47, false);
#line 11 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Item\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
            EndContext();
            BeginContext(291, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(341, 43, false);
#line 14 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Item\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Description));

#line default
#line hidden
            EndContext();
            BeginContext(384, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(433, 43, false);
#line 17 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Item\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.GPValue));

#line default
#line hidden
            EndContext();
            BeginContext(476, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(526, 39, false);
#line 20 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Item\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.GPValue));

#line default
#line hidden
            EndContext();
            BeginContext(565, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(614, 40, false);
#line 23 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Item\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Slot));

#line default
#line hidden
            EndContext();
            BeginContext(654, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(704, 39, false);
#line 26 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Item\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Slot.ID));

#line default
#line hidden
            EndContext();
            BeginContext(743, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(792, 60, false);
#line 29 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Item\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.ConstructionRequirements));

#line default
#line hidden
            EndContext();
            BeginContext(852, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(902, 56, false);
#line 32 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Item\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.ConstructionRequirements));

#line default
#line hidden
            EndContext();
            BeginContext(958, 18, true);
            WriteLiteral("\r\n    </dd>\r\n</dl>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Item> Html { get; private set; }
    }
}
#pragma warning restore 1591
