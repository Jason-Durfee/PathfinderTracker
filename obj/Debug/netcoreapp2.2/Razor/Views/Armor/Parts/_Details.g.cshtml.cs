#pragma checksum "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Armor\Parts\_Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cc24b754a5c53a90df34f0c25ec266a1380f228b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Armor_Parts__Details), @"mvc.1.0.view", @"/Views/Armor/Parts/_Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Armor/Parts/_Details.cshtml", typeof(AspNetCore.Views_Armor_Parts__Details))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc24b754a5c53a90df34f0c25ec266a1380f228b", @"/Views/Armor/Parts/_Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"147e504006a9f7463f793a5e88f3c13df5b5652d", @"/Views/_ViewImports.cshtml")]
    public class Views_Armor_Parts__Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Armor>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(14, 55, true);
            WriteLiteral("\r\n<dl class=\"row\">\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(70, 43, false);
#line 5 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Armor\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.GPValue));

#line default
#line hidden
            EndContext();
            BeginContext(113, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(163, 39, false);
#line 8 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Armor\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.GPValue));

#line default
#line hidden
            EndContext();
            BeginContext(202, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(251, 44, false);
#line 11 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Armor\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Material));

#line default
#line hidden
            EndContext();
            BeginContext(295, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(345, 43, false);
#line 14 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Armor\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Material.ID));

#line default
#line hidden
            EndContext();
            BeginContext(388, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(437, 45, false);
#line 17 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Armor\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.ArmorType));

#line default
#line hidden
            EndContext();
            BeginContext(482, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(532, 46, false);
#line 20 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Armor\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.ArmorType.Name));

#line default
#line hidden
            EndContext();
            BeginContext(578, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(627, 46, false);
#line 23 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Armor\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.ArmorAddon));

#line default
#line hidden
            EndContext();
            BeginContext(673, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(723, 45, false);
#line 26 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Armor\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.ArmorAddon.ID));

#line default
#line hidden
            EndContext();
            BeginContext(768, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(817, 53, false);
#line 29 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Armor\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.SpecialAttributes));

#line default
#line hidden
            EndContext();
            BeginContext(870, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(920, 49, false);
#line 32 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Armor\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.SpecialAttributes));

#line default
#line hidden
            EndContext();
            BeginContext(969, 20, true);
            WriteLiteral("\r\n    </dd>\r\n</dl>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Armor> Html { get; private set; }
    }
}
#pragma warning restore 1591
