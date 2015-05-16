using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Data
{
    using System.Text.RegularExpressions;
    using Models;
    using Repositories;

    public class TwitterData : ITwitterData
    {
        private ITwitterDbContext context;
        private IDictionary<Type, Object> repositories;

        public TwitterData(ITwitterDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Tweet> Tweets
        {
            get
            {
                return this.GetRepository<Tweet>();
            }
        }

        public IRepository<Notification> Notifications
        {
            get
            {
                return this.GetRepository<Notification>();
            }
        }

        public IRepository<Profile> Profiles
        {
            get
            {
                return this.GetRepository<Profile>();
            }
        }

        public IRepository<Follow> Follows
        {
            get
            {
                return this.GetRepository<Follow>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!this.repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<T>);
//                if (type.IsAssignableFrom(typeof(User)))
//                {
//                    repositoryType = typeof(UserRepository);
//                }
                this.repositories.Add(typeof(T), Activator.CreateInstance(repositoryType, this.context));
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}
