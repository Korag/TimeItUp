using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeItUpData.Library.Models
{
    public class Timer
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Split> Splits { get; set; }
        public virtual ICollection<Alarm> Alarms { get; set; }
    }
}
