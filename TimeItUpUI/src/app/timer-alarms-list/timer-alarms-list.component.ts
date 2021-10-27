import { Component, Input, OnInit } from '@angular/core';
import { AlarmModel, TimerModel } from '../_models';
import { AlarmService } from '../_services';

@Component({
  selector: 'app-timer-alarms-list',
  templateUrl: './timer-alarms-list.component.html',
  styleUrls: ['./timer-alarms-list.component.scss']
})
export class TimerAlarmsListComponent implements OnInit {
  @Input() timer!: TimerModel;
  alarms: AlarmModel[] = [];
  listLoading: boolean = true;

  constructor(private alarmService: AlarmService) { }

  async ngOnInit(): Promise<void> {
    this.alarms = await this.alarmService.getTimerAlarms(this.timer.id!);
    this.listLoading = false;
  }
}
