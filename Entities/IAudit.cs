using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public interface IAudit
    {
        DateTime CreatedDate { get; set; }

        DateTime? LastUpdatedDate { get; set; }

        string LastUpdatedBy { get; set; }

        string CreatedBy { get; set; }
    }
}
