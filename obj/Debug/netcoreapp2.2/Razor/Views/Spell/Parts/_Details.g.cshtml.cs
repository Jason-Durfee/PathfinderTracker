#pragma checksum "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5b7f6cf746a9e6adba0474ab864f9aa317749748"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Spell_Parts__Details), @"mvc.1.0.view", @"/Views/Spell/Parts/_Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Spell/Parts/_Details.cshtml", typeof(AspNetCore.Views_Spell_Parts__Details))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5b7f6cf746a9e6adba0474ab864f9aa317749748", @"/Views/Spell/Parts/_Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"147e504006a9f7463f793a5e88f3c13df5b5652d", @"/Views/_ViewImports.cshtml")]
    public class Views_Spell_Parts__Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Spell>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(14, 55, true);
            WriteLiteral("\r\n<dl class=\"row\">\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(70, 40, false);
#line 5 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(110, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(160, 36, false);
#line 8 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(196, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(245, 47, false);
#line 11 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.MagicSchool));

#line default
#line hidden
            EndContext();
            BeginContext(292, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(342, 48, false);
#line 14 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.MagicSchool.Name));

#line default
#line hidden
            EndContext();
            BeginContext(390, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(439, 47, false);
#line 17 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
            EndContext();
            BeginContext(486, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(536, 43, false);
#line 20 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Description));

#line default
#line hidden
            EndContext();
            BeginContext(579, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(628, 47, false);
#line 23 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.CastingTime));

#line default
#line hidden
            EndContext();
            BeginContext(675, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(725, 43, false);
#line 26 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.CastingTime));

#line default
#line hidden
            EndContext();
            BeginContext(768, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(817, 49, false);
#line 29 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.RangeDistance));

#line default
#line hidden
            EndContext();
            BeginContext(866, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(916, 45, false);
#line 32 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.RangeDistance));

#line default
#line hidden
            EndContext();
            BeginContext(961, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(1010, 40, false);
#line 35 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Area));

#line default
#line hidden
            EndContext();
            BeginContext(1050, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(1100, 36, false);
#line 38 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Area));

#line default
#line hidden
            EndContext();
            BeginContext(1136, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(1185, 42, false);
#line 41 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Target));

#line default
#line hidden
            EndContext();
            BeginContext(1227, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(1277, 38, false);
#line 44 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Target));

#line default
#line hidden
            EndContext();
            BeginContext(1315, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(1364, 44, false);
#line 47 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Duration));

#line default
#line hidden
            EndContext();
            BeginContext(1408, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(1458, 40, false);
#line 50 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Duration));

#line default
#line hidden
            EndContext();
            BeginContext(1498, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(1547, 47, false);
#line 53 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.SavingThrow));

#line default
#line hidden
            EndContext();
            BeginContext(1594, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(1644, 43, false);
#line 56 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.SavingThrow));

#line default
#line hidden
            EndContext();
            BeginContext(1687, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(1736, 51, false);
#line 59 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.SpellResistance));

#line default
#line hidden
            EndContext();
            BeginContext(1787, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(1837, 47, false);
#line 62 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Spell\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.SpellResistance));

#line default
#line hidden
            EndContext();
            BeginContext(1884, 18, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Spell> Html { get; private set; }
    }
}
#pragma warning restore 1591
