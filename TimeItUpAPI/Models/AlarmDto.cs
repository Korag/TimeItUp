using System;

namespace TimeItUpAPI.Models
{
    public class AlarmDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ActivationTime { get; set; }
    }
}
