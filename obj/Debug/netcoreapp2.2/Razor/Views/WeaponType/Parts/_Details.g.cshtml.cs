#pragma checksum "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "063950fd26a3241e95bd69252c115538189660ff"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_WeaponType_Parts__Details), @"mvc.1.0.view", @"/Views/WeaponType/Parts/_Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/WeaponType/Parts/_Details.cshtml", typeof(AspNetCore.Views_WeaponType_Parts__Details))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"063950fd26a3241e95bd69252c115538189660ff", @"/Views/WeaponType/Parts/_Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"147e504006a9f7463f793a5e88f3c13df5b5652d", @"/Views/_ViewImports.cshtml")]
    public class Views_WeaponType_Parts__Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WeaponType>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(19, 55, true);
            WriteLiteral("\r\n<dl class=\"row\">\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(75, 40, false);
#line 5 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(115, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(165, 36, false);
#line 8 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(201, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(250, 46, false);
#line 11 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.DamageType));

#line default
#line hidden
            EndContext();
            BeginContext(296, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(346, 42, false);
#line 14 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.DamageType));

#line default
#line hidden
            EndContext();
            BeginContext(388, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(437, 44, false);
#line 17 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.HasReach));

#line default
#line hidden
            EndContext();
            BeginContext(481, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(531, 40, false);
#line 20 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.HasReach));

#line default
#line hidden
            EndContext();
            BeginContext(571, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(620, 51, false);
#line 23 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.AttackDiceSmall));

#line default
#line hidden
            EndContext();
            BeginContext(671, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(721, 47, false);
#line 26 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.AttackDiceSmall));

#line default
#line hidden
            EndContext();
            BeginContext(768, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(817, 52, false);
#line 29 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.AttackDiceMedium));

#line default
#line hidden
            EndContext();
            BeginContext(869, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(919, 48, false);
#line 32 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.AttackDiceMedium));

#line default
#line hidden
            EndContext();
            BeginContext(967, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(1016, 44, false);
#line 35 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Critical));

#line default
#line hidden
            EndContext();
            BeginContext(1060, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(1110, 40, false);
#line 38 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Critical));

#line default
#line hidden
            EndContext();
            BeginContext(1150, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(1199, 43, false);
#line 41 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.GPValue));

#line default
#line hidden
            EndContext();
            BeginContext(1242, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(1292, 39, false);
#line 44 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.GPValue));

#line default
#line hidden
            EndContext();
            BeginContext(1331, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(1380, 47, false);
#line 47 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.AttackRange));

#line default
#line hidden
            EndContext();
            BeginContext(1427, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(1477, 43, false);
#line 50 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.AttackRange));

#line default
#line hidden
            EndContext();
            BeginContext(1520, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(1569, 42, false);
#line 53 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Weight));

#line default
#line hidden
            EndContext();
            BeginContext(1611, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(1661, 38, false);
#line 56 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Weight));

#line default
#line hidden
            EndContext();
            BeginContext(1699, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(1748, 50, false);
#line 59 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.WeaponCategory));

#line default
#line hidden
            EndContext();
            BeginContext(1798, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(1848, 51, false);
#line 62 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.WeaponCategory.Name));

#line default
#line hidden
            EndContext();
            BeginContext(1899, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(1948, 50, false);
#line 65 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.WeaponCoreType));

#line default
#line hidden
            EndContext();
            BeginContext(1998, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(2048, 51, false);
#line 68 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\WeaponType\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.WeaponCoreType.Name));

#line default
#line hidden
            EndContext();
            BeginContext(2099, 18, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WeaponType> Html { get; private set; }
    }
}
#pragma warning restore 1591
