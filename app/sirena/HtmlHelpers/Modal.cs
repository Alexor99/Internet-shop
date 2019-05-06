using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.HtmlHelpers
{
    public class Modal
    {
        public static IHtmlContent DeleteModal()
        {
            var modal = @"<!-- Bootstrap modal popup -->
                          <div class=""modal fade"" id=""modalFade"" tabindex="" - 1"" role=""dialog"" aria-labelledby=""myModalLabel"">
                                < div class=""modal-dialog"" role=""document"">
                                  <div class=""modal-content"">
                                      <div class=""modal-header alert alert-danger"">
                                          <button type = ""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                                              <span aria-hidden=""true"">&times;</span>
                                          </button>
                                          <h4 class=""modal-title"" id=""myModalLabel"">Infromation Dialog</h4>
                                      </div>
                                      <div class=""modal-body"">
                                          <p class=""success-message"">Are you sure you wish to delete this record? </p>
                                      </div>
                                      <div class=""modal-footer"">
                                          <button class=""btn btn-success delete-confirm"">Delete</button>
                                          <button class=""btn btn-default delete-calcel"" data-dismiss=""modal"">Cancel</button>
                                          <button class=""invisible btn btn-default delete-done"" data-dismiss=""modal"">OK</button>
                                      </div>
                                  </div>
                              </div>
                          
                          <!-- End of the boostrap modal popup -->

                          <script src = ""/basejs/deleteModal.js""></script>";

            return new HtmlString(modal);
        }

    }
}
