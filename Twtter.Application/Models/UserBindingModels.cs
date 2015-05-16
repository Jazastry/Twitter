using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twtter.Application.Models
{
    using Twitter.Models;

    public class UserTweetsOutputModel
    {
        public string AuthorId { get; set; }
        public string AuthorUserName { get; set; }
        public IEnumerable<TweetOutputModel> Tweets { get; set; } 
    }
}