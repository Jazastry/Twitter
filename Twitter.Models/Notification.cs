namespace Twitter.Models
{
    using Constants;

    public class Notification
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public virtual User User { get; set; }

        public NotificationType Type { get; set; }
    }
}
