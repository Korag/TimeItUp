import { Component, Input, OnInit, SimpleChange } from '@angular/core';
import { Router } from '@angular/router';
import { PauseModel, TimerModel } from '../_models';
import { PauseService } from '../_services';

@Component({
  selector: 'app-timer-pauses-list',
  templateUrl: './timer-pauses-list.component.html',
  styleUrls: ['./timer-pauses-list.component.scss']
})
export class TimerPausesListComponent implements OnInit {
  @Input() timer!: TimerModel;
  @Input() addedPause!: PauseModel;

  @Input() pauseChildMessage: string = "";

  pauses: PauseModel[] = [];
  listLoading: boolean = true;

  constructor(private router: Router,
              private pauseService: PauseService) { }

  async ngOnInit(): Promise<void> {

    console.log("INIT");

    this.pauses = await this.pauseService.getTimerPauses(this.timer.id!);
    this.listLoading = false;
  }

  async ngOnChanges(changes: { [property: string]: SimpleChange }) {
    let changeOfNewlyAddedPause: SimpleChange = changes['addedPause'];
    let messageFromParent: SimpleChange = changes['pauseChildMessage'];

    console.log(changeOfNewlyAddedPause);
    console.log(messageFromParent);

    if (changeOfNewlyAddedPause !== undefined) {
      if (this.pauseChildMessage !== "" && changeOfNewlyAddedPause.previousValue! !== changeOfNewlyAddedPause.currentValue) {
        //this.pauses.push(this.addedPause);

        this.listLoading = true;
        await this.ngOnInit();
        this.listLoading = false;
      }
    }

    if (messageFromParent !== undefined && this.pauseChildMessage === "finish") {
      //this.pauses.pop();
      await this.recalculatePauseTotalDuration();
      //this.pauses.push(await this.pauseService.getPauseById(this.addedPause.id!));

      this.listLoading = true;
      await this.ngOnInit();
      this.listLoading = false;
    }
  }

  async recalculatePauseTotalDuration() {
    await this.pauseService.calculatePauseTotalDuration(this.addedPause.id!);
  }
}
