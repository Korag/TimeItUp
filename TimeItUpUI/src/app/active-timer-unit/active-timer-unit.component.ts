import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription, timer as rxTimer } from 'rxjs';
import { map, share } from 'rxjs/operators';
import { PauseModel, SplitModel, TimerModel } from '../_models';
import { AuthService, PauseService, SplitService, TimerService } from '../_services';

@Component({
  selector: 'app-active-timer-unit',
  templateUrl: './active-timer-unit.component.html',
  styleUrls: ['./active-timer-unit.component.scss']
})
export class ActiveTimerUnitComponent implements OnInit {
  @Input() timer!: TimerModel;
  @Output() finishTimerEvent = new EventEmitter<TimerModel>();

  countDownTime = new Date();
  intervalId!: any;
  subscription!: any;

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
    //if (this.timer.totalCountdownTimer !== "0d:0h:0m:0s:0ms") {
    //  this.subscription = rxTimer(0, 1)
    //    .pipe(
    //      map(() => new Date()),
    //      share()
    //    )
    //    .subscribe(time => {
    //      this.countDownTime = time;
    //    });
    //}

    //set isStarted variable based on countdown and startAt value
    this.isPaused = this.timer.paused!;

    if (this.isPaused) {
      this.pause = await this.pauseService.getTimerActivePause(this.timer.id!);
    }
    if (this.isStarted && !this.isPaused) {
      this.split = await this.splitService.getTimerActiveSplit(this.timer.id!);
    }

    console.log(this.timer);
  }

  async startTimer() {
    await this.timerService.startTimer(this.timer.id!);
    this.split = await this.splitService.getTimerActiveSplit(this.timer.id!);
    this.toastr.success('The timer has been started');

    //start time count
  }

  async finishTimer() {
    this.finishTimerEvent.emit(this.timer);
  }

  async pauseTimer() {
    this.pause = await this.pauseService.createPause(this.timer.id!);
    await this.pauseService.startPause(this.pause.id!);
    this.isPaused = true;
    this.toastr.warning('The timer has been paused');

    //stop subscription on countdown
  }

  async resumeTimer() {
    await this.pauseService.finishPause(this.pause.id!);
    this.isPaused = false;
    this.toastr.info('The timer has been resumed');

    //resume countdown
  }

  async createSplit() {
    await this.splitService.finishSplit(this.split.id!);
    this.split = await this.splitService.createSplit(this.timer.id!);
    await this.splitService.startSplit(this.split.id!);
    this.toastr.info('New split has been created');

    //nothing happens with countdown time
  }
}
