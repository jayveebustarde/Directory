using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Discount : BaseEntity
    {
        public string Title { get; set; }

        public decimal PercentDiscount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Details { get; set; }

        #region Navigation
        public ICollection<ProductDiscount> ProductDiscounts { get; set; }
        #endregion
    }
}
