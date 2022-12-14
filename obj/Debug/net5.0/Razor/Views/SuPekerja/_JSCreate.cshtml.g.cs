#pragma checksum "C:\Users\HP\source\repos\SUMBER\Views\SuPekerja\_JSCreate.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "13236f651f2f6fe42a515da5d25ff6493fab9e1a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SuPekerja__JSCreate), @"mvc.1.0.view", @"/Views/SuPekerja/_JSCreate.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"13236f651f2f6fe42a515da5d25ff6493fab9e1a", @"/Views/SuPekerja/_JSCreate.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd51267031d8a1db8b377745dfa23d3c5ef6e639", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_SuPekerja__JSCreate : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<script type=""text/javascript"">

    $(""body"").on(""click"", ""#btnAdd1"", function () {
        //Reference the Name and Country TextBoxes.
        var txtNama = $(""#txtNama"");
        var txtNokp = $(""#txtNokp"");
        var txtHubungan = $(""#txtHubungan"");

        //Get the reference of the Table's TBODY element.
        var tBody = $(""#tblObjek > TBODY"")[0];
        //Add Row.
        var check = false;
        var table = $(""#tblObjek tbody"");
        table.find('tr').each(function (i, el) {
            var $tds = $(this).find('td');
            var loop = $tds.eq(0).text();
            if (txtNokp.val().toString().trim() === loop.toString().trim()) {
                check = true;
            };
        });
        if (!check && txtNama.val().length > 0 && txtNokp.val().length > 0 && txtHubungan.val().length > 0) {
            var row = tBody.insertRow(-1);

            //Add col1 cell.
            var cell = $(row.insertCell(-1));
            cell.html(txtNokp.val());
            ");
            WriteLiteral(@"cell.prop(""hidden"", !this.checked);

            //Add col2 cell.
            var cell = $(row.insertCell(-1));
            cell.html(txtNama.val());

            //Add col3 cell.
            var cell = $(row.insertCell(-1));
            cell.html(txtNokp.val());

            //Add col4 cell.
            var cell = $(row.insertCell(-1));
            cell.html(txtHubungan.val());

            //Add col5 cell.
            cell = $(row.insertCell(-1));
            var btnRemove1 = $(""<button class='btn btn-danger btn-sm' type='button' id='btnRemove1' onclick='Remove1(this)'><i class='fas fa-trash'></i></button>"");
            cell.append(btnRemove1);

            var tanggungan = {
                NoKP: txtNokp.val(),
                Nama: txtNama.val(),
                Hubungan: txtHubungan.val()
            }

            if (txtNokp.val()) {
                $.ajax({
                    type: ""POST"",
                    url: ""/SuPekerja/SaveTanggungan"",
                    data: ta");
            WriteLiteral(@"nggungan,
                    dataType: ""json"",
                    success: function (r) {
                        //Clear the TextBoxes.
                        txtNama.val('');
                        txtNokp.val('');
                        txtHubungan.val('');
                    },
                    error: function (req, status, error) {
                        alert(error);
                    }
                });
            }
        };
    });

    function Remove1(button) {
        //Determine the reference of the Row using the Button.
        var row = $(button).closest(""TR"");
        var nama = $(""TD"", row).eq(1).html();
        var nokp = $(""TD"", row).eq(0).html();
        if (confirm(""Hapus maklumat : "" +nokp+"" - ""+ nama + ""?"")) {
            //Get the reference of the Table.
            var table = $(""#tblObjek"")[0];

            var tanggungan = {
                NoKP: nokp,
            }
            if (name != null) {
                $.ajax({
               ");
            WriteLiteral(@"     type: ""POST"",
                    url: ""/SuPekerja/RemoveTanggungan"",
                    data: tanggungan,
                    dataType: ""json"",
                    success: function (r) {
                        //Delete the Table row using it's Index.
                        table.deleteRow(row[0].rowIndex);
                    }
                });
            }
        }
    };

    //validation form
    (function () {
        'use strict';
        window.addEventListener('load', function () {
            // Fetch all the forms we want to apply custom Bootstrap validation styles to
            var forms = document.getElementsByClassName('needs-validation');
            // Loop over them and prevent submission
            Array.prototype.filter.call(forms, function (form) {
                form.addEventListener('submit', function (event) {
                    if (form.checkValidity() === false) {
                        event.preventDefault();
                        event.sto");
            WriteLiteral(@"pPropagation();
                    }
                    form.classList.add('was-validated');
                }, false);
            });
        }, false);
    })();

    $(document).ready(function () {
        $.validator.setDefaults({
            errorElement: ""span"",
            errorClass: ""help-block"",
            //	validClass: 'stay',
            highlight: function (element, errorClass) {
                $(element).addClass(errorClass); //.removeClass(errorClass);
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            },
            unhighlight: function (element, errorClass) {
                $(element).removeClass(errorClass); //.addClass(validClass);
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
            },

            errorPlacement: function (error, element) {
                if (element.parent('.input-group').length) {
                    error.insertAfter(e");
            WriteLiteral("lement.parent());\r\n                } else {\r\n                    error.insertAfter(element);\r\n                }\r\n\r\n            },\r\n\r\n        });\r\n    });\r\n    //validation form end\r\n\r\n</script>");
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
