#pragma checksum "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Player\Parts\_Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "50285386672eced2e683e39f5711a71c52e3bc29"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Player_Parts__Details), @"mvc.1.0.view", @"/Views/Player/Parts/_Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Player/Parts/_Details.cshtml", typeof(AspNetCore.Views_Player_Parts__Details))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"50285386672eced2e683e39f5711a71c52e3bc29", @"/Views/Player/Parts/_Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"147e504006a9f7463f793a5e88f3c13df5b5652d", @"/Views/_ViewImports.cshtml")]
    public class Views_Player_Parts__Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Player>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(15, 55, true);
            WriteLiteral("\r\n<dl class=\"row\">\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(71, 41, false);
#line 5 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Player\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.HPMax));

#line default
#line hidden
            EndContext();
            BeginContext(112, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(162, 37, false);
#line 8 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Player\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.HPMax));

#line default
#line hidden
            EndContext();
            BeginContext(199, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(248, 45, false);
#line 11 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Player\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.HPCurrent));

#line default
#line hidden
            EndContext();
            BeginContext(293, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(343, 41, false);
#line 14 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Player\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.HPCurrent));

#line default
#line hidden
            EndContext();
            BeginContext(384, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(433, 45, false);
#line 17 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Player\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Character));

#line default
#line hidden
            EndContext();
            BeginContext(478, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(528, 46, false);
#line 20 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Player\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Character.Name));

#line default
#line hidden
            EndContext();
            BeginContext(574, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(623, 43, false);
#line 23 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Player\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Bonuses));

#line default
#line hidden
            EndContext();
            BeginContext(666, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(716, 39, false);
#line 26 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Player\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Bonuses));

#line default
#line hidden
            EndContext();
            BeginContext(755, 48, true);
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
            EndContext();
            BeginContext(804, 40, false);
#line 29 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Player\Parts\_Details.cshtml"
   Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(844, 49, true);
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
            EndContext();
            BeginContext(894, 36, false);
#line 32 "C:\Users\profd\Desktop\Projects\PathfinderTracker\PathfinderTracker\Views\Player\Parts\_Details.cshtml"
   Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(930, 18, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Player> Html { get; private set; }
    }
}
#pragma warning restore 1591