using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twtter.Application.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TweetsPageModel
    {
        public string AuthorSortParm { get; set; }

        public string DateSortParm { get; set; }

        public string CurrentSort { get; set; }

        public string CurrentFilter { get; set; }

        public int? Page { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public IEnumerable<TweetOutputModel> Tweets { get; set; } 
    }
}