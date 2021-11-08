import { Component, OnInit } from '@angular/core';
import { TimerModel } from '../_models';
import { AuthService, TimerService } from '../_services';

@Component({
  selector: 'app-past-timers',
  templateUrl: './past-timers.component.html',
  styleUrls: ['./past-timers.component.scss']
})
export class PastTimersComponent implements OnInit {
  timers: TimerModel[] = [];
  listLoading: boolean = true;

  constructor(
    private authService: AuthService,
    private timerService: TimerService) { }

  async ngOnInit(): Promise<void> {
    this.timers = await this.timerService.getUserPastTimers(this.authService.loggedUserData.id!);
    this.listLoading = false;
  }
}

