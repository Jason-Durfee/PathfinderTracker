#pragma checksum "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\ArmorAddonType\Parts\_Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9225420bccd93c7abda1c29ac1b87ce784945a50"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ArmorAddonType_Parts__Details), @"mvc.1.0.view", @"/Views/ArmorAddonType/Parts/_Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ArmorAddonType/Parts/_Details.cshtml", typeof(AspNetCore.Views_ArmorAddonType_Parts__Details))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9225420bccd93c7abda1c29ac1b87ce784945a50", @"/Views/ArmorAddonType/Parts/_Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"147e504006a9f7463f793a5e88f3c13df5b5652d", @"/Views/_ViewImports.cshtml")]
    public class Views_ArmorAddonType_Parts__Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ArmorAddonType>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(23, 55, true);
            WriteLiteral("\r\n<dl class=\"row\">\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(79, 40, false);
#line 5 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\ArmorAddonType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(119, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(169, 36, false);
#line 8 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\ArmorAddonType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(205, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(254, 47, false);
#line 11 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\ArmorAddonType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.BaseGPValue));

#line default
#line hidden
            EndContext();
            BeginContext(301, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(351, 43, false);
#line 14 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\ArmorAddonType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.BaseGPValue));

#line default
#line hidden
            EndContext();
            BeginContext(394, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(443, 53, false);
#line 17 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\ArmorAddonType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.ArmorCheckPenalty));

#line default
#line hidden
            EndContext();
            BeginContext(496, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(546, 49, false);
#line 20 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\ArmorAddonType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.ArmorCheckPenalty));

#line default
#line hidden
            EndContext();
            BeginContext(595, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(644, 42, false);
#line 23 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\ArmorAddonType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Weight));

#line default
#line hidden
            EndContext();
            BeginContext(686, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(736, 38, false);
#line 26 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\ArmorAddonType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Weight));

#line default
#line hidden
            EndContext();
            BeginContext(774, 18, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ArmorAddonType> Html { get; private set; }
    }
}
#pragma warning restore 1591
