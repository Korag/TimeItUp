using System.Collections.Generic;

namespace TimeItUpAPI.Models
{
    public class CompleteTimerInfoDto
    {
        public TimerDto Timer { get; set; }
        public ICollection<SplitDto> Splits { get; set; }
        public ICollection<PauseDto> Pauses { get; set; }
        public ICollection<AlarmDto> Alarms { get; set; }
    }
}
