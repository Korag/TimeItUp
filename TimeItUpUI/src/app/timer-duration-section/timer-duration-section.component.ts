import { Component, Input, OnInit } from '@angular/core';
import { CountdownTimeModel, TimerModel } from '../_models';

@Component({
  selector: 'app-timer-duration-section',
  templateUrl: './timer-duration-section.component.html',
  styleUrls: ['./timer-duration-section.component.scss']
})
export class TimerDurationSectionComponent implements OnInit {
  @Input() timer!: TimerModel;
  loading = false;

  totalDurationTime: CountdownTimeModel = new CountdownTimeModel();
  totalPauseTime: CountdownTimeModel = new CountdownTimeModel();

  totalDurationIntervalId!: any;
  totalPauseIntervalId!: any;

  isPaused!: boolean;
  isStarted!: boolean;
  isFinished!: boolean;

  constructor() { }

  async ngOnInit(): Promise<void> {

    if (this.timer.startAt?.toString() === "0001-01-01T00:00:00") {
      this.isStarted = false;
    }
    else {
      this.isStarted = true;
    }

    await this.calculateTime(this.totalDurationTime, this.timer.totalDuration);
    await this.calculateTime(this.totalPauseTime, this.timer.totalPausedTime);

    this.isPaused = this.timer.paused!;
    this.isFinished = this.timer.finished!;

    console.log(this.timer);

    if (this.isPaused) {
      await this.startPauseDurationCountdown();
    }
    if (this.isStarted && !this.isFinished) {
      await this.startTotalDurationCountdown();
    }
  }

  async calculateTime(timeModel: CountdownTimeModel, timeMagicString: any) {
    var timeSplitted = timeMagicString?.split(":");

    timeModel.hours = parseInt(timeSplitted![0]);
    timeModel.minutes = parseInt(timeSplitted![1]);
    timeModel.seconds = parseInt(timeSplitted![2]);
    timeModel.miliseconds = parseInt(timeSplitted![3]);
  }

  async runningTimeCountdown(countdownModel: CountdownTimeModel) {
    if (countdownModel.miliseconds !== 999) {
      countdownModel.miliseconds!++;
      return;
    }
    else {
      if (countdownModel.seconds !== 59) {
        countdownModel.seconds!++;
        countdownModel.miliseconds! = 0;
        return;
      }
      else {
        if (countdownModel.minutes !== 59) {
          countdownModel.minutes!++;
          countdownModel.seconds! = 0;
          countdownModel.miliseconds! = 0;
          return;
        }
        else {
          countdownModel.hours!++;
          countdownModel.minutes! = 0;
          countdownModel.seconds! = 0;
          countdownModel.miliseconds! = 0;
          return;
        }
      }
    }
  }

  async startTotalDurationCountdown() {
    this.totalDurationIntervalId = setInterval(this.runningTimeCountdown.bind(this), 1, this.totalDurationTime);
  }

  async pauseTotalDurationCountdown() {
    clearInterval(this.totalDurationIntervalId);
  }

  async startPauseDurationCountdown() {
    this.totalPauseIntervalId = setInterval(this.runningTimeCountdown.bind(this), 1, this.totalPauseTime);
  }

  async pausePauseDurationCountdown() {
    clearInterval(this.totalPauseIntervalId);
  }

  async startTimer() {
    await this.startTotalDurationCountdown();
  }

  async finishTimer() {
    await this.pauseTotalDurationCountdown();
    await this.pausePauseDurationCountdown();
  }

  async pauseTimer() {
    await this.startPauseDurationCountdown();
  }

  async resumeTimer() {
    await this.pausePauseDurationCountdown();
  }
}
