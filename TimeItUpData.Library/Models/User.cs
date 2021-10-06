using System.Collections.Generic;

namespace TimeItUpData.Library.Models
{
    public class User
    {
        public string Id { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Timer> Timers { get; set; }
    }
}
