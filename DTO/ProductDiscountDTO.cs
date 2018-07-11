using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductDiscountDTO : BaseDTO
    {
        public Guid ProductId { get; set; }

        public Guid DiscountId { get; set; }

        public Guid? VariationId { get; set; }
        
        public ProductDTO Product { get; set; }

        public DiscountDTO Discount { get; set; }

        public VariationDTO Variation { get; set; }
    }
}
