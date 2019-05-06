using sirena.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.ViewModels
{
    public class ColorVM
    {
        [ScaffoldColumn(false)]
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Is Disabled")]
        public bool IsDisabled { get; set; }

        public int? SOrder { get; set; }

        public List<ProductVM> Products { get; set; }
    }
}
