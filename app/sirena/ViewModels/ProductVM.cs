using sirena.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.ViewModels
{
    public class ProductVM
    {
        [ScaffoldColumn(false)]
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Article { get; set; }

        public double Cost { get; set; }

        public string Description { get; set; }

        public string MainPhoto { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Is Disabled")]
        public bool IsDisabled { get; set; }
        
        public List<CategoryVM> Categories { get; set; }
        public List<ColorVM> Colors { get; set; }
        public List<SizeVM> Sizes { get; set; }

    }
}
