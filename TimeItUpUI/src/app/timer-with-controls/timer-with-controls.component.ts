import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CountdownTimeModel, PauseModel, SplitModel, TimerModel } from '../_models';
import { PauseService, SplitService, TimerService } from '../_services';

@Component({
  selector: 'app-timer-with-controls',
  templateUrl: './timer-with-controls.component.html',
  styleUrls: ['./timer-with-controls.component.scss']
})
export class TimerWithControlsComponent implements OnInit {
  @Input() timer!: TimerModel;

  @Output() startTimerEvent = new EventEmitter<void>();
  @Output() finishTimerEvent = new EventEmitter<TimerModel>();

  @Output() pauseTimerEvent = new EventEmitter<PauseModel>();
  @Output() finishPauseTimerEvent = new EventEmitter<void>();

  @Output() splitTimerEvent = new EventEmitter<SplitModel>();
  @Output() finishSplitTimerEvent = new EventEmitter<void>();

  countdownTime: CountdownTimeModel = new CountdownTimeModel();
  intervalId!: any;

  isPaused!: boolean;
  isStarted!: boolean;
  isFinished!: boolean;

  pause!: PauseModel;
  split!: SplitModel;

  constructor(
    private timerService: TimerService,
    private pauseService: PauseService,
    private splitService: SplitService,
    private toastr: ToastrService) { }

  async ngOnInit(): Promise<void> {

    if (this.timer.startAt?.toString() === "0001-01-01T00:00:00") {
      this.isStarted = false;
    }
    else {
      this.isStarted = true;
    }

    await this.calculateCountdownTime();
    this.isPaused = this.timer.paused!;
    this.isFinished = this.timer.finished!;

    if (this.isPaused) {
      this.pause = await this.pauseService.getTimerActivePause(this.timer.id!);
    }
    if (this.isStarted && !this.isPaused && !this.isFinished) {
      this.split = await this.splitService.getTimerActiveSplit(this.timer.id!);

      await this.startTimerCountdown();
    }
  }

  async calculateCountdownTime() {
    var countdownTimeSplitted = await this.timer.totalCountdownTime?.split(":");

    this.countdownTime.hours = parseInt(countdownTimeSplitted![0]);
    this.countdownTime.minutes = parseInt(countdownTimeSplitted![1]);
    this.countdownTime.seconds = parseInt(countdownTimeSplitted![2]);
    this.countdownTime.miliseconds = parseInt(countdownTimeSplitted![3]);
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
    this.intervalId = await setInterval(this.runningCountdown.bind(this), 1);
  }

  async pauseTimerCountdown() {
    await clearInterval(this.intervalId);
  }

  async startTimer() {
    await this.timerService.startTimer(this.timer.id!);
    this.split = await this.splitService.getTimerActiveSplit(this.timer.id!);
    this.isStarted = true;
    this.toastr.success('The timer has been started');

    await this.splitTimerEvent.emit(this.split);
    await this.startTimerEvent.emit();

    await this.startTimerCountdown();
  }

  async finishTimer() {
    this.isFinished = true;
    await this.pauseTimerCountdown();

    await this.finishTimerEvent.emit(this.timer);
  }

  async reinstateTimer() {
    await this.timerService.reinstateTimer(this.timer.id!);
    this.toastr.success('The timer has been reinstated');
    this.isFinished = false;

    this.split = await this.splitService.getTimerActiveSplit(this.timer.id!);
    await this.splitTimerEvent.emit(this.split);
    await this.startTimerEvent.emit();

    await this.startTimerCountdown()
  }

  async pauseTimer() {
    this.pause = await this.pauseService.createPause(this.timer.id!);
    await this.pauseService.startPause(this.pause.id!);

    var now = new Date();
    this.pause.startAt = new Date(now.toUTCString().slice(0, -4));
    this.isPaused = true;
    this.toastr.warning('The timer has been paused');

    await this.pauseTimerEvent.emit(this.pause);
    await this.finishSplitTimerEvent.emit();

    await this.pauseTimerCountdown();
  }

  async resumeTimer() {
    await this.pauseService.finishPause(this.pause.id!);
    this.isPaused = false;
    this.split = await this.splitService.getTimerActiveSplit(this.timer.id!);
    this.toastr.info('The timer has been resumed');

    await this.splitTimerEvent.emit(this.split);
    await this.finishPauseTimerEvent.emit();

    await this.startTimerCountdown();
  }

  async createSplit() {
    await this.splitService.finishSplit(this.split.id!);
    await this.finishSplitTimerEvent.emit();

    this.split = await this.splitService.createSplit(this.timer.id!);
    await this.splitService.startSplit(this.split.id!);

    var now = new Date();
    this.split.startAt = new Date(now.toUTCString().slice(0, -4));
    this.toastr.info('New split has been created');

    await this.splitTimerEvent.emit(this.split);
  }
}
