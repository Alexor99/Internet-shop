using sirena.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.ViewModels
{
    public class CategoryVM
    {
        [ScaffoldColumn(false)]
        public Guid? Id { get; set; }

        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Is Disabled")]
        public bool IsDisabled { get; set; }

        public List<ProductVM> Products { get; set; }
    }
}
