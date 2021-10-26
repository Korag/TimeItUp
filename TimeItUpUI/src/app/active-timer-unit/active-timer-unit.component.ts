import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CountdownTimeModel, PauseModel, SplitModel, TimerModel } from '../_models';
import { AuthService, PauseService, SplitService, TimerService } from '../_services';

@Component({
  selector: 'app-active-timer-unit',
  templateUrl: './active-timer-unit.component.html',
  styleUrls: ['./active-timer-unit.component.scss']
})
export class ActiveTimerUnitComponent implements OnInit {
  @Input() timer!: TimerModel;
  @Output() finishTimerEvent = new EventEmitter<TimerModel>();

  countdownTime: CountdownTimeModel = new CountdownTimeModel();
  intervalId!: any;

  isPaused!: boolean;
  isStarted!: boolean;

  pause!: PauseModel;
  split!: SplitModel;

  constructor(private router: Router,
    private authService: AuthService,
    private timerService: TimerService,
    private pauseService: PauseService,
    private splitService: SplitService,
    private toastr: ToastrService) { }

  async ngOnInit(): Promise<void> {
    console.log(this.timer);

    if (this.timer.startAt?.toString() === "0001-01-01T00:00:00") {
      this.isStarted = false;
    }
    else {
      this.isStarted = true;
    }

    var countdownTimeSplitted = this.timer.totalCountdownTime?.split(":");
    this.countdownTime.hours = parseInt(countdownTimeSplitted![0]);
    this.countdownTime.minutes = parseInt(countdownTimeSplitted![1]);
    this.countdownTime.seconds = parseInt(countdownTimeSplitted![2]);
    this.countdownTime.miliseconds = parseInt(countdownTimeSplitted![3]);

    this.isPaused = this.timer.paused!;

    if (this.isPaused) {
      this.pause = await this.pauseService.getTimerActivePause(this.timer.id!);
    }
    if (this.isStarted && !this.isPaused) {
      this.split = await this.splitService.getTimerActiveSplit(this.timer.id!);

      await this.startTimerCountdown();
    }
  }

  async runningCountdown() {
    if (this.countdownTime.miliseconds !== 999) {
      this.countdownTime.miliseconds!++;
      return;
    }
    else {
      if (this.countdownTime.seconds !== 59) {
        this.countdownTime.seconds!++;
        this.countdownTime.miliseconds! = 0;
        return;
      }
      else {
        if (this.countdownTime.minutes !== 59) {
          this.countdownTime.minutes!++;
          this.countdownTime.seconds! = 0;
          this.countdownTime.miliseconds! = 0;
          return;
        }
        else {
          this.countdownTime.hours!++;
          this.countdownTime.minutes! = 0;
          this.countdownTime.seconds! = 0;
          this.countdownTime.miliseconds! = 0;
          return;
        }
      }
    }
  }

  async startTimerCountdown() {
    this.intervalId = setInterval(this.runningCountdown, 1);
  }

  async pauseTimerCountdown() {
    clearInterval(this.intervalId);
  }

  async startTimer() {
    await this.timerService.startTimer(this.timer.id!);
    this.split = await this.splitService.getTimerActiveSplit(this.timer.id!);
    this.toastr.success('The timer has been started');

    await this.startTimerCountdown();
  }

  async finishTimer() {
    this.finishTimerEvent.emit(this.timer);
  }

  async pauseTimer() {
    this.pause = await this.pauseService.createPause(this.timer.id!);
    await this.pauseService.startPause(this.pause.id!);
    this.isPaused = true;
    this.toastr.warning('The timer has been paused');

    await this.pauseTimerCountdown();
  }

  async resumeTimer() {
    await this.pauseService.finishPause(this.pause.id!);
    this.isPaused = false;
    this.toastr.info('The timer has been resumed');

    await this.startTimerCountdown();
  }

  async createSplit() {
    await this.splitService.finishSplit(this.split.id!);
    this.split = await this.splitService.createSplit(this.timer.id!);
    await this.splitService.startSplit(this.split.id!);
    this.toastr.info('New split has been created');
  }
}

//Add name to split
