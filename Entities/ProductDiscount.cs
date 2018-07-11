using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ProductDiscount : BaseEntity
    {
        public Guid ProductId { get; set; }

        public Guid DiscountId { get; set; }

        public Guid? VariationId { get; set; }

        #region Navigation
        public Product Product { get; set; }

        public Discount Discount { get; set; }

        public Variation Variation { get; set; }

        #endregion
    }
}
