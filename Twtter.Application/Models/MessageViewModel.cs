using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twtter.Application.Constants;

namespace Twtter.Application.Models
{
    public class MessageViewModel
    {
        private string text = "";
        public MesageType Type { get; set; }

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }
    }
}