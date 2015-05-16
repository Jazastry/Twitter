using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twtter.Application.Controllers
{
    using Twitter.Data;
    using Twitter.Data.Repositories;

    public class BaseController : Controller
    {
        private ITwitterData data;

        public ITwitterData Data
        {
            get { return this.data; }
        }

        public BaseController()
            : this(new TwitterData(new TwitterDbContext()))
        {
            
        }

        public BaseController(ITwitterData data)
        {
            this.data = data;
        }
    }
}