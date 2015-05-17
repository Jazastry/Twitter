namespace Twtter.Application.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using PagedList;

    using Models;
    using Twitter.Data;


    public class HomeController : BaseController
    {
        private ITwitterData data;

        public HomeController()
            : this(new TwitterData(new TwitterDbContext()))
        {
            
        }

        public HomeController(ITwitterData data)
        {
            this.data = data;
        }
        public ActionResult Index(string sortOrder, string searchString, int? page)
        {
            ViewBag.AuthorSortParm = String.IsNullOrEmpty(sortOrder) ? "Author" : "";
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = searchString;

            var tweets = this.Data.Tweets.All()
                .Include(t => t.User)
                .Select(t => new TweetOutputModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    AuthorName = t.User.UserName,
                    AuthorId = t.UserId,
                    Text = t.Text,
                    DateCreated = t.DateCreated
                });

            if (!String.IsNullOrEmpty(searchString))
            {
                tweets = tweets.Where(t => t.AuthorName.Contains(searchString)
                                       || t.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Author":
                    tweets = tweets.OrderBy(t => t.AuthorName);
                    break;
                case "Date":
                    tweets = tweets.OrderByDescending(t => t.DateCreated);
                    break;
                default:
                    tweets = tweets.OrderBy(t => t.Id);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(tweets.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}