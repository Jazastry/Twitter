namespace Twitter.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Follow
    {
        [Key]
        public int Id { get; set; }

        public string FollowerId { get; set; }

        public virtual User Follower { get; set; }
 
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }
    }
}
