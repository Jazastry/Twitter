using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twtter.Application.Models
{
    using Twitter.Models;

    public class TweetOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime DateCreated { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }
    }
}