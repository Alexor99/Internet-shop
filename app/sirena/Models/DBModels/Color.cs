using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.Models.DBModels
{
    public class Color
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? IsDisabled { get; set; }
        public int? SOrder { get; set; }

        public virtual ICollection<ProductColor> ProductColor { get; set; }
    }
}
