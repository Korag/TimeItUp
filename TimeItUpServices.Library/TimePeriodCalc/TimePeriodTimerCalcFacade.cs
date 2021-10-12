﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeItUpData.Library.Models;

namespace TimeItUpServices.Library
{
    public class TimePeriodTimerCalcFacade : ITimePeriodTimerCalcFacade
    {
        public ITimeAndDateHelper _timeHelper { get; set; }

        public TimePeriodTimerCalcFacade(ITimeAndDateHelper timeHelper)
        {
            _timeHelper = timeHelper;
        }

        public Pause CalculatePauseTimePeriod(Pause pause)
        {
            pause.TotalDuration = _timeHelper.CalculateDateTimePeriodAsString(pause.StartAt, pause.EndAt);
            pause.TotalDurationTimeSpan = _timeHelper.CalculateDateTimePeriodAsTimeSpan(pause.StartAt, pause.EndAt);

            return pause;
        }

        public Split CalculateSplitTimePeriod(Split split)
        {
            split.TotalDuration = _timeHelper.CalculateDateTimePeriodAsString(split.StartAt, split.EndAt);
            split.TotalDurationTimeSpan = _timeHelper.CalculateDateTimePeriodAsTimeSpan(split.StartAt, split.EndAt);

            return split;
        }

        public Timer CalculateTimerTimePeriods(Timer timer)
        {
            timer.TotalDurationTimeSpan = _timeHelper.CalculateDateTimePeriodAsTimeSpan(timer.StartAt, timer.EndAt);
            timer.TotalDuration = _timeHelper.CalculateDateTimePeriodAsString(timer.StartAt, timer.EndAt);

            timer.TotalPausedTimeSpan = _timeHelper.AddTimeSpans(timer.Pauses.Select(z => z.TotalDurationTimeSpan).ToList());
            timer.TotalPausedTime = _timeHelper.CalculateDateTimePeriodAsString(timer.TotalPausedTimeSpan);

            timer.TotalCountdownTimeSpan = _timeHelper.AddTimeSpans(timer.Splits.Select(z => z.TotalDurationTimeSpan).ToList());
            timer.TotalCountdownTime = _timeHelper.CalculateDateTimePeriodAsString(timer.TotalCountdownTimeSpan);

            return timer;
        }
    }
}
