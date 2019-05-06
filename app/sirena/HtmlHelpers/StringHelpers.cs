using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.HtmlHelpers
{
    public static class StringHelpers
    {
        public static IHtmlContent CutController(this string controllerName)
       => new HtmlString(controllerName.ToLower().Split("controller")[0]);
    }
}
