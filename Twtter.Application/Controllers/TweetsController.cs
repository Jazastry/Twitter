using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twtter.Application.Controllers
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity;
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
                    tweets = tweets.OrderBy(t => t.Id);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (model.Page ?? 1);

            pageModel.Tweets = tweets;
            pageModel.PageSize = pageSize;
            pageModel.PageNumber = pageNumber;
            pageModel.ActionName = "Index";
            pageModel.ControllerName = "Home";
            pageModel.CurrentSort = model.SortOrder;

            return this.PartialView(pageModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Title = "Create Tweet";

            return this.View();
        }

        [HttpPost]
        [Authorize]
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

                var tweetSaved = this.Data.Tweets.Find(t => t.Equals(tweet)).FirstOrDefault();
                tweet.Url = GetBaseUrl() + "Tweets/" + tweetSaved.Id;

                this.Data.Tweets.Update(tweet);
                this.Data.SaveChanges();
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