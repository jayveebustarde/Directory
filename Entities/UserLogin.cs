using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserLogin
    {
        [Column(Order = 0), Key]
        public Guid UserId { get; set; }

        [Column(Order = 1), Key]
        public string LoginProvider { get; set; }

        [Column(Order = 2), Key]
        public string ProviderKey { get; set; }

        public User User { get; set; }
    }
}
