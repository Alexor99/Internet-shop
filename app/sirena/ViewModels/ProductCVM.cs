using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.ViewModels
{
    public class ProductCVM
    {
        [ScaffoldColumn(false)]
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string MainPhoto { get; set; }

        public string Article { set; get; }

        public double Cost { get; set; }

        public string Description { get; set; }

        public IFormFile File { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Is Disabled")]
        public bool IsDisabled { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public Guid[] SelectedCategories { get; set; }

        public IEnumerable<SelectListItem> Colors { get; set; }
        public Guid[] SelectedColors { get; set; }

        public IEnumerable<SelectListItem> Sizes { get; set; }
        public Guid[] SelectedSizes { get; set; }
    }
}
