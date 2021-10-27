import { Component, Input, OnInit } from '@angular/core';
import { PauseModel, TimerModel } from '../_models';
import { PauseService } from '../_services';

@Component({
  selector: 'app-timer-pauses-list',
  templateUrl: './timer-pauses-list.component.html',
  styleUrls: ['./timer-pauses-list.component.scss']
})
export class TimerPausesListComponent implements OnInit {
  @Input() timer!: TimerModel;
  pauses: PauseModel[] = [];
  listLoading: boolean = true;

  constructor(private pauseService: PauseService,) { }

  async ngOnInit(): Promise<void> {
    this.pauses = await this.pauseService.getTimerPauses(this.timer.id!);
    this.listLoading = false;
  }
}
