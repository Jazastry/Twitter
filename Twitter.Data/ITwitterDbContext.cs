namespace Twitter.Data.Repositories
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Text.RegularExpressions;
    using Models;

    public interface ITwitterDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Tweet> Tweets { get; set; }

        IDbSet<Notification> Notifications { get; set; }

        IDbSet<Profile> Profiles { get; set; }

        IDbSet<Follow> Follows { get; set; } 

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
