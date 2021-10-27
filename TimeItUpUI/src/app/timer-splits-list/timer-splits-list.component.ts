import { Component, Input, OnInit } from '@angular/core';
import { SplitModel, TimerModel } from '../_models';
import { SplitService } from '../_services';

@Component({
  selector: 'app-timer-splits-list',
  templateUrl: './timer-splits-list.component.html',
  styleUrls: ['./timer-splits-list.component.scss']
})
export class TimerSplitsListComponent implements OnInit {
  @Input() timer!: TimerModel;
  splits: SplitModel[] = [];
  listLoading: boolean = true;

  constructor(private splitService: SplitService,) { }

  async ngOnInit(): Promise<void> {
    this.splits = await this.splitService.getTimerSplits(this.timer.id!);
    this.listLoading = false;
  }
}
