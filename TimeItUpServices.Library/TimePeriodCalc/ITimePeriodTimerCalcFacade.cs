using TimeItUpData.Library.Models;

namespace TimeItUpServices.Library
{
    public interface ITimePeriodTimerCalcFacade
    {
        public Pause CalculatePauseTimePeriod(Pause pause);
        public Split CalculateSplitTimePeriod(Split split);
        public Timer CalculateTimerTimePeriods(Timer timer);
    }
}
