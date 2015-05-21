using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twtter.Application.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Twitter.Models;

    public class TweetInputModel
    {
        [Required(ErrorMessage = "Tweet text is obligatory.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Tweet text is obligatory.")]
        [MinLength(1)]
        public string Text { get; set; }
    }
}
