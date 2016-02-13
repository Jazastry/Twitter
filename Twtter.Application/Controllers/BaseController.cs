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

    public abstract class BaseController : Controller
    {
        private ITwitterData data;

        protected ITwitterData Data { get; private set; }

        public BaseController(ITwitterData data)
        {
            this.Data = data;
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