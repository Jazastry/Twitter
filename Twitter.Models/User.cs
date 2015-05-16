namespace Twitter.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public User()
        {
            this.Tweets = new HashSet<Tweet>();
            this.Follows = new HashSet<Follow>();
        }

        public int ProfileId { get; set; }

        public virtual Profile UserProfile { get; set; }

        public virtual ICollection<Tweet> Tweets { get; set; }

        public virtual ICollection<Follow> Follows { get; set; } 
    }
}
