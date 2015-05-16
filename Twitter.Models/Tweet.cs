using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Models
{
    using Microsoft.Win32;

    public class Tweet
    {
        public Tweet()
        {
            this.Answers = new HashSet<Tweet>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Tweet> Answers { get; set; }
    }
}
