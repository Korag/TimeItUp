import { Component, Input, OnInit, SimpleChange } from '@angular/core';
import { SplitModel, TimerModel } from '../_models';
import { SplitService } from '../_services';

@Component({
  selector: 'app-timer-splits-list',
  templateUrl: './timer-splits-list.component.html',
  styleUrls: ['./timer-splits-list.component.scss']
})
export class TimerSplitsListComponent implements OnInit {
  @Input() timer!: TimerModel;
  @Input() addedSplit!: SplitModel;

  @Input() splitChildMessage: string = "";

  splits: SplitModel[] = [];
  listLoading: boolean = true;

  constructor(private splitService: SplitService) { }

  async ngOnInit(): Promise<void> {

    this.splits = await this.splitService.getTimerSplits(this.timer.id!);
    this.listLoading = false;
  }

  async ngOnChanges(changes: { [property: string]: SimpleChange }) {
    let changeOfNewlyAddedSplit: SimpleChange = changes['addedSplit'];
    let messageFromParent: SimpleChange = changes['splitChildMessage'];

    if (changeOfNewlyAddedSplit !== undefined) {
      if (this.splitChildMessage !== "" && changeOfNewlyAddedSplit.previousValue! !== changeOfNewlyAddedSplit.currentValue) {
        //this.splits.push(this.addedSplit);

        this.listLoading = true;
        await this.ngOnInit();
        this.listLoading = false;
      }
    }

    if (messageFromParent !== undefined && this.splitChildMessage === "finish") {
      //this.splits.pop();
      await this.recalculateSplitTotalDuration();
      //this.splits.push(await this.splitService.getSplitById(this.addedSplit.id!));

      this.listLoading = true;
      await this.ngOnInit();
      this.listLoading = false;
    }
  }

  async recalculateSplitTotalDuration() {
    await this.splitService.calculateSplitTotalDuration(this.addedSplit.id!);
  }
}
