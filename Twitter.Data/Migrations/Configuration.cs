namespace Twitter.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Twitter.Data.TwitterDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Twitter.Data.TwitterDbContext context)
        {
            if (! context.Users.Any())
            {
                User one = new User()
                {
                    UserName = "Gogo",
                    Email = "main@main.com",
                    PasswordHash = "770f31f6-3750-41c8-b512-652ad5f11814"
                };
                context.Users.Add(one);
                User two = new User()
                {
                    UserName = "Mogo",
                    Email = "main@main.com",
                    PasswordHash = "770f31f6-3750-41c8-b512-652ad5f11814"
                };
                context.Users.Add(two);
                User three = new User()
                {
                    UserName = "Vogo",
                    Email = "main@main.com",
                    PasswordHash = "770f31f6-3750-41c8-b512-652ad5f11814"
                };
                context.Users.Add(three);
                Tweet twOne = new Tweet()
                {
                    User = one,
                    Text = "First tweet text",
                    Title = "Tweet One",
                };
                context.Tweets.Add(twOne);
                Tweet twTwo = new Tweet()
                {
                    User = two,
                    Text = "Second tweet text",
                    Title = "Tweet Two",
                };
                //ivo@piskov.net :::::  CV SEMINAR  ::::::::::::::
                context.Tweets.Add(twTwo);

                Tweet twThree = new Tweet()
                {
                    User = three,
                    Text = "4 tweet text",
                    Title = "Tweet Three",
                };
                context.Tweets.Add(twThree);
                Tweet twFour = new Tweet()
                {
                    User = one,
                    Text = "5 tweet text",
                    Title = "Tweet One",
                };
                context.Tweets.Add(twFour);

                Tweet twFive = new Tweet()
                {
                    User = two,
                    Text = "6 tweet text",
                    Title = "Tweet Two",
                };
                context.Tweets.Add(twFive);
                Tweet twSix = new Tweet()
                {
                    User = three,
                    Text = "7 tweet text",
                    Title = "Tweet Three",
                };
                context.Tweets.Add(twSix);

                Follow folowOne = new Follow()
                {
                    Author = one,
                    Follower = two
                };
                context.Follows.Add(folowOne);
                Follow folowTwo = new Follow()
                {
                    Author = one,
                    Follower = three
                };
                context.Follows.Add(folowTwo);
                Follow folowThree = new Follow()
                {
                    Author = two,
                    Follower = one
                };
                context.Follows.Add(folowThree);
                Follow folowFour = new Follow()
                {
                    Author = two,
                    Follower = three
                };
                context.Follows.Add(folowFour);
                Follow folowFive = new Follow()
                {
                    Author = three,
                    Follower = one
                };
                context.Follows.Add(folowFive);
                Follow folowSix = new Follow()
                {
                    Author = three,
                    Follower = two
                };
                context.Follows.Add(folowSix);

                context.SaveChanges();
            }
        }
    }
}
