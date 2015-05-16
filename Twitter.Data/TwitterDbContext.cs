namespace Twitter.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Text.RegularExpressions;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Repositories;

    public class TwitterDbContext : IdentityDbContext<User>, ITwitterDbContext
    {

        public TwitterDbContext()
            : base("TwitterDb", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TwitterDbContext, Twitter.Data.Migrations.Configuration>());
        }


        public IDbSet<Tweet> Tweets { get; set; }

        public IDbSet<Profile> Profiles { get; set; }

        public IDbSet<Notification> Notifications { get; set; }

        public IDbSet<Follow> Follows { get; set; } 

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public static TwitterDbContext Create()
        {
            return new TwitterDbContext();
        }
    }
}
