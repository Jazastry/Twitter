using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twtter.Application.Controllers
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity;
    using Models;
    using Twitter.Data;

    public class UsersController : BaseController
    {
        public UsersController()
            : this(new TwitterData(new TwitterDbContext()))
        {

        }

        public UsersController(ITwitterData data)
            : base(data)
        {

        }

        public ActionResult Index(string username)
        {
            var tweets = this.Data.Follows.All()
                .Include(u => u.Author)
                .Include(u => u.Author.Tweets)
                .Where(f => f.Follower.UserName == username)
                .Select(t => t.Author)
                .Select(a => a.Tweets.Select(t => new TweetOutputModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Text = t.Text,
                    UserName = t.User.UserName
                })).ToList();

            
            

            return this.View(tweets[0]);
        }

        // GET: User
//        public ActionResult Profile(string username)
//        {
//            var user = this.Data.Users.All().Where(u => u.UserName == username).FirstOrDefault();
//            return this.View(user);
//        }

//        public ActionResult GetFile()
//        {
//            var pers = new { 
//                name = "Goshko",
//                age = 12
//            };
//            return Json(pers, JsonRequestBehavior.AllowGet);
//        }
    }
}