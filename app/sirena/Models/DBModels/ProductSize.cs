using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.Models.DBModels
{
    public class ProductSize
    {
        public Guid ProductId { get; set; }

        public Guid SizeId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("SizeId")]
        public Size Size { get; set; }
    }
}
