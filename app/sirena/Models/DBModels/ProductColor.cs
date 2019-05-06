using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.Models.DBModels
{
    public class ProductColor
    {
        public Guid ProductId { get; set; }

        public Guid ColorId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("ColorId")]
        public Color Color { get; set; }
    }
}
