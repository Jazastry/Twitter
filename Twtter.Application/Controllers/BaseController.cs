using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twtter.Application.Controllers
{
    using System.Data.Entity;
    using Models;
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

        protected string GetBaseUrl()
        {                            //.Current.Request
            var request = HttpContext.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (!string.IsNullOrWhiteSpace(appUrl)) appUrl += "/";

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }
    }
}