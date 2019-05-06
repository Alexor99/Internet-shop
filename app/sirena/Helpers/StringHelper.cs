using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirena.Helpers
{
    public static class StringHelper
    {
        public static string CutController(this string controllerName)
        {
            return controllerName.ToLower().Split("controller")[0];
        }

        public static string URIBuilder(string areaName, string controllerName, string actionName)
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(areaName))
                sb.Append($"/{areaName.ToLower()}");

            if (!string.IsNullOrWhiteSpace(controllerName))
                sb.Append($"/{controllerName.CutController()}");

            if (!string.IsNullOrWhiteSpace(actionName))
                sb.Append($"/{actionName.CutController()}");

            return sb.ToString();
        }

        public static string GetFileName(this string path)
        {
            try
            {
                string str = string.Empty;
                int index = path.LastIndexOf('/');

                if (index != 1)
                {
                    str = path.Substring(index+1);
                }

                return str;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}
