using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.Models.DBModels
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Article { set; get; }
        public double Cost { get; set; }
        public string Description { get; set; }
        public string MainPhoto { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? IsDisabled { get; set; }

        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
        public virtual ICollection<ProductColor> ProductColor { get; set; }
        public virtual ICollection<ProductSize> ProductSize { get; set; }
    }
}
