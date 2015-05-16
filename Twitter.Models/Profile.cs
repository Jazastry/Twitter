using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Profile
    {
        public int Id { get; set; }

        [Key, ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
