using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductDTO : BaseDTO
    {
        public string Name { get; set; }

        public decimal RegularPrice { get; set; }

        public string Description { get; set; }

        public Guid ProductTypeId { get; set; }


        public ProductTypeDTO ProductType { get; set; }

        public List<VariationDTO> Variations { get; set; }
    }
}
