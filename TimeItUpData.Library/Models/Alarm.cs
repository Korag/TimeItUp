namespace TimeItUpData.Library.Models
{
    public class Alarm
    {
        public string Id { get; set; }

        public string TimerId { get; set; }
        public virtual Timer Timer { get; set; }
    }
}
