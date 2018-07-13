using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class VariationDTO : BaseDTO
    {
        public Guid ProductId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public ProductDTO Product { get; set; }
    }
}