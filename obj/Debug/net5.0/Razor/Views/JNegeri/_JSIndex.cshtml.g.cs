#pragma checksum "C:\Users\HP\source\repos\SUMBER\Views\JNegeri\_JSIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b373c39342c39e6d82c90405724ee450ab004199"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_JNegeri__JSIndex), @"mvc.1.0.view", @"/Views/JNegeri/_JSIndex.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\HP\source\repos\SUMBER\Views\_ViewImports.cshtml"
using SUMBER;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\HP\source\repos\SUMBER\Views\_ViewImports.cshtml"
using SUMBER.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\HP\source\repos\SUMBER\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\HP\source\repos\SUMBER\Views\_ViewImports.cshtml"
using SUMBER.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\HP\source\repos\SUMBER\Views\_ViewImports.cshtml"
using System.Threading.Tasks;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\HP\source\repos\SUMBER\Views\_ViewImports.cshtml"
using SUMBER.Models.Administration;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\HP\source\repos\SUMBER\Views\_ViewImports.cshtml"
using Microsoft.EntityFrameworkCore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\HP\source\repos\SUMBER\Views\_ViewImports.cshtml"
using System.Globalization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\HP\source\repos\SUMBER\Views\_ViewImports.cshtml"
using SUMBER.Infrastructure;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b373c39342c39e6d82c90405724ee450ab004199", @"/Views/JNegeri/_JSIndex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd51267031d8a1db8b377745dfa23d3c5ef6e639", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_JNegeri__JSIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<script type=""text/javascript"">
    var columnSearchVar;
    $(document).ready(function () {

        var table = $('.table').DataTable({
            ""paging"": false,
            rowReorder: true,
            columnDefs: [
                { orderable: true, className: 'reorder', targets: 1 },
                { orderable: true, className: 'reorder', targets: 2 },
                { orderable: false, targets: '_all' }
            ],
            ""order"": [[1, ""asc""]],
            ""scrollY"": ""50vh"",
            ""scrollX"": true,
            ""scrollCollapse"": true,
            ""paging"": false,
            dom: 'lrt'
        });

        $('#columnSearch').change(function () {
            columnSearchVar = $(this).val();
            $('#myInput').val('');
            table.destroy();

            table = $('.table').DataTable({
                ""paging"": false,
                rowReorder: true,
                columnDefs: [
                    { orderable: true, className: 'reorder', targe");
            WriteLiteral(@"ts: 1 },
                    { orderable: true, className: 'reorder', targets: 2 },
                    { orderable: false, targets: '_all' }
                ],
                ""order"": [[1, ""asc""]],
                ""scrollY"": ""50vh"",
                ""scrollX"": true,
                ""scrollCollapse"": true,
                ""paging"": false,
                dom: 'lrt'
            });
        });

        table.on('order.dt search.dt', function () {
            table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                cell.innerHTML = i + 1;
            });
        }).draw();

        $('#myInput').on('keyup', function () {
            if (!columnSearchVar) {
                table
                    .search(this.value)
                    .draw();
            }
            else {
                table
                    .columns(columnSearchVar)
                    .search(this.value)
                    .draw();
            }
     ");
            WriteLiteral("   });\r\n    });\r\n</script>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public ApplicationDbContext _context { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<IdentityUser> UserManager { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<IdentityUser> SignInManager { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
