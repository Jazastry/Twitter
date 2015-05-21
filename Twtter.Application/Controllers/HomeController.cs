namespace Twtter.Application.Controllers
{
    using System.Web.Mvc;

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


        public ActionResult Index(TweetsControllerModel model)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Users");
            }

            var tweetsControllerData = new TweetsControllerModel()
            {
                CurrentFilter = model.CurrentFilter,
                SortOrder = model.SortOrder,
                Page = model.Page
            };

            return View(tweetsControllerData);
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