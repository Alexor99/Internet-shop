using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.HtmlHelpers
{
    public class ButtonHelpers
    {
        public static IHtmlContent CancelBtn(string areaName, string controllerName, string actionName, string tagClass)
        {
            return Btn("Cancel", areaName, controllerName, actionName, tagClass);
        }

        public static IHtmlContent Btn(string buttonText, string areaName, string controllerName, string actionName, string tagClass)
        {
            var tag = $"<input type=\"button\" " +
                      $"value=\"{buttonText}\" " +
                      $"class=\"{tagClass.ToLower()}\" " +
                      $"onclick=\"window.location.href = '{Helpers.StringHelper.URIBuilder(areaName, controllerName, actionName)}'\" />";

            return new HtmlString(tag);
        }

    }
}
