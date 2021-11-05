import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PauseModel, SplitModel, TimerModel } from '../_models';
import { PauseService, SplitService, TimerService } from '../_services';

@Component({
  selector: 'app-timer-details',
  templateUrl: './timer-details.component.html',
  styleUrls: ['./timer-details.component.scss']
})
export class TimerDetailsComponent implements OnInit {
  timer!: TimerModel;
  addedPause!: PauseModel
  addedSplit!: SplitModel;

  splitChildMessage!: string;
  pauseChildMessage!: string;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private timerService: TimerService,
    private splitService: SplitService,
    private pauseService: PauseService,
    private toastr: ToastrService) { }

  async ngOnInit(): Promise<void> {
    await this.timerService.calculateSelectedTimerPeriods(this.route.snapshot.params.id);
    this.timer = await this.timerService.getTimerById(this.route.snapshot.params.id);

    var activeSplit = await this.splitService.getTimerActiveSplit(this.timer.id!);
    var activePause = await this.pauseService.getTimerActivePause(this.timer.id!);

    if (activeSplit !== null) {
      this.splitChildMessage = "no push";
      this.addedSplit = activeSplit;
      this.splitChildMessage = "";
    }

    if (activePause !== null) {
      this.pauseChildMessage = "no push";
      this.addedPause = activePause;
      this.pauseChildMessage = "";
    }
  }

  async finishTimer(timer: TimerModel): Promise<void> {
    await this.timerService.finishTimer(timer.id!);
    await this.finishPause(event);
    await this.finishSplit(event);

    this.toastr.success('The timer has been terminated');
  }

  async pauseTimer(pause: PauseModel): Promise<void> {
    this.addedPause = pause;
  }

  async splitTimer(split: SplitModel): Promise<void> {
    this.addedSplit = split;
  }

  async finishSplit($event: any): Promise<void> {
    this.splitChildMessage = "";
    this.splitChildMessage = "finish";
  }

  async finishPause($event: any): Promise<void> {
    this.pauseChildMessage = "";
    this.pauseChildMessage = "finish";
  }
}
