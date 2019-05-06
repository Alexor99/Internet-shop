using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.Models.DBModels
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? IsDisabled { get; set; }

        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
