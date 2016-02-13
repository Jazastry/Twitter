namespace Twtter.Application.Controllers
{
    using System.Web.Mvc;

    using Models;
    using Twitter.Data;


    public class HomeController : BaseController
    {

        public HomeController()
            : base(new TwitterData(new TwitterDbContext()))
        {
            
        }

        public HomeController(ITwitterData data) 
            : base(data)
        {
        }


        public ActionResult Index(TweetsControllerModel model)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Users");
            }


            return View(model);
        }
    }
}