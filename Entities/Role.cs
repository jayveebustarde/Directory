using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Role : IRole<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
