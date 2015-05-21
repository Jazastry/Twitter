using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twtter.Application.Models
{
    public class TweetsControllerModel
    {
        public string SortOrder { get; set; }

        public string CurrentFilter { get; set; }

        public int? Page { get; set; }
    }
}