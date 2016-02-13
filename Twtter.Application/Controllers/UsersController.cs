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

    [Authorize]
    public class UsersController : BaseController
    {
        public UsersController()
            : base(new TwitterData(new TwitterDbContext()))
        {

        }

        public UsersController(ITwitterData data)
            : base(data)
        {

        }

        public ActionResult Index(TweetsControllerModel model)
        {
            this.ViewBag.User = this.User.Identity.GetUserName();
         

            return View(model);
        }

        public ActionResult All()
        {
            var users = this.Data.Users.All()
                .Select(u => new UserOutputModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Picture = u.Picture
                })
                .ToList();

            return this.View(users);
        }

        [HttpGet]
        public ActionResult GetUser(string id)
        {
            var user = this.Data.Users.All()
                .Where(u => u.Id.Equals(id))
                .Select(u => new UserOutputModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Picture = u.Picture
                })
                .FirstOrDefault();

            if (user != null)
            {
                return this.PartialView("/DisplayTemplates/_UserPopup", user);
            }

            return null;
        }

        public ActionResult Profile(string userId)
        {
            return null;
        }
    }
}