using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public decimal RegularPrice { get; set; }

        public string Description { get; set; }

        public Guid ProductTypeId { get; set; }

        #region Navigation
        public virtual ProductType ProductType { get; set; }

        public ICollection<Variation> Variations { get; set; }

        public ICollection<ProductDiscount> ProductDiscounts { get; set; }

        #endregion
    }
}
