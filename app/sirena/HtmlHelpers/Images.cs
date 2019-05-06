using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sirena.Helpers;

namespace sirena.HtmlHelpers
{
    public class Images
    {
        public static IHtmlContent UploadImage(string id, string imageLink = null, string imgTagId = null, string name = null)
        {
            name = name == null ? id : name;
            imgTagId = imgTagId == null ? "outputFile" : imgTagId;

            var tag = $"<input " +
                      $"class=\"form-control valid\" " +
                      $"type=\"file\" " +
                      $"onchange=\"preview_image(event, '{imgTagId}')\" " +
                      $"id=\"{id}\" " +
                      $"name=\"{name}\" " +
                      $"aria-invalid=\"false\"> " +
                      $"<img id=\"{imgTagId}\" src=\"{imageLink}\" style=\"max-width: 300px; margin: 20px 0 20px 0\"\" /> " +
                      $"<script src=\"/basejs/previewImages.js\"></script>";

            return new HtmlString(tag);
        }
    }
}
