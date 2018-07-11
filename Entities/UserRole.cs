using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserRole
    {
        [Column(Order = 0), Key]
        public Guid UserId { get; set; }

        [Column(Order = 1), Key]
        public Guid RoleId { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    }
}
