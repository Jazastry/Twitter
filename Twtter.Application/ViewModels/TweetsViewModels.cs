using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twtter.Application.ViewModels
{
    using Twitter.Models;

    public class TweetsOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public string AuthorId { get; set; }

        public UserOutputModel Author { get; set; }
    }
}