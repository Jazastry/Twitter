using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twtter.Application.Controllers
{
    using System.Data.Entity;
    using Hubs;
    using Microsoft.Ajax.Utilities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin.Security.Provider;
    using Models;
    using PagedList;
    using Twitter.Data;
    using Twitter.Models;
    using WebGrease.Css.Extensions;

    public class TweetsController : BaseController
    {
         private ITwitterData data;

        public TweetsController()
            : this(new TwitterData(new TwitterDbContext()))
        {
            
        }

        public TweetsController(ITwitterData data)
        {
            this.data = data;
        }

        // GET: Tweets
        public ActionResult Index(TweetsControllerModel model)
        {
            var loggedUser = this.User.Identity.IsAuthenticated;

            var tweets = this.TweetsFacade();

            var pageModel = new TweetsPageModel()
            {
                AuthorSortParm = "Author",
                DateSortParm = "Date",
                CurrentSort = model.SortOrder,
                CurrentFilter = model.CurrentFilter,
                Page = model.Page
            };

            if (!String.IsNullOrEmpty(model.CurrentFilter))
            {
                tweets = tweets.Where(t => t.AuthorName.Contains(model.CurrentFilter)
                                       || t.Title.Contains(model.CurrentFilter));
            }

            switch (model.SortOrder)
            {
                case "Author":
                    tweets = tweets.OrderBy(t => t.AuthorName);
                    break;
                case "Date":
                    tweets = tweets.OrderByDescending(t => t.DateCreated);
                    break;
                default:
                    tweets = tweets.OrderByDescending(t => t.DateCreated);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (model.Page ?? 1);

            pageModel.Tweets = tweets.ToPagedList(pageNumber, pageSize);
            pageModel.PageSize = pageSize;
            pageModel.PageNumber = pageNumber;
            pageModel.ActionName = "Index";
            pageModel.ControllerName = "Home";
            pageModel.CurrentSort = model.SortOrder;

            return this.PartialView(pageModel);
        }

        [HttpGet]
        public ActionResult GetPartialTweet(int? id)
        {
            var tweet = this.Data.Tweets.All()
                .Where(t => t.Id == id)
                .Select(t => new TweetOutputModel()
                {
                    Id = t.Id,
                    Text = t.Text,
                    Title = t.Title,
                    AuthorId = t.UserId,
                    AuthorName = t.User.UserName,
                    DateCreated = t.DateCreated
                })
                .FirstOrDefault();

            return this.PartialView(tweet);
        }

        [HttpGet]
        [System.Web.Mvc.Authorize]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [System.Web.Mvc.Authorize]
        public ActionResult Create(TweetInputModel model)
        {
            if (this.ModelState != null && this.ModelState.IsValid )
            {
                var tweet = new Tweet()
                {
                    Title = model.Title,
                    Text = model.Text,
                    UserId = this.User.Identity.GetUserId(),
                    DateCreated = DateTime.Now
                };

                this.Data.Tweets.Add(tweet);
                this.Data.SaveChanges();

                var hub = GlobalHost.ConnectionManager.GetHubContext<TweeterHub>();
                hub.Clients.All.showTweet(tweet.Id);


                return this.RedirectToAction("Index", "Users");
            }

            return this.View(model);
        }

        [NonAction]
        private IQueryable<TweetOutputModel> TweetsFacade()
        {
            var loggedUser = this.User.Identity.IsAuthenticated;

            if (loggedUser)
            {
                return UserFollowedTweets();
            }

            return AllTweets();
        }

        [NonAction]
        private IQueryable<TweetOutputModel> AllTweets()
        {
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

            return tweets;
        }

        [NonAction]
        private IQueryable<TweetOutputModel> UserFollowedTweets()
        {
            var userId = this.User.Identity.GetUserId();
            var userTweetModel = this.Data.Follows.All()
                .Include(f => f.Author)
                .Include(f => f.Author.Tweets)
                .Where(f => f.FollowerId == userId)
                .Select(f => new UserTweetsOutputModel()
                {
                    Tweets = f.Author.Tweets.Select(t => new TweetOutputModel()
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Text = t.Text,
                        DateCreated = t.DateCreated,
                        AuthorId = t.UserId,
                        AuthorName = t.User.UserName,
                    })
                }).ToList();


            var result = new List<TweetOutputModel>();

            foreach (var model in userTweetModel)
            {
                result.AddRange(model.Tweets);   
            }

            return result.AsQueryable();
        }
    }
}