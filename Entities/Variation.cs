using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Variation : BaseEntity
    {
        public Guid ProductId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        #region Navigation
        public Product Product { get; set; }
        #endregion
    }
}
