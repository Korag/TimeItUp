import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TimerModel } from '../_models';
import { AuthService, TimerService } from '../_services';

@Component({
  selector: 'app-active-timers',
  templateUrl: './active-timers.component.html',
  styleUrls: ['./active-timers.component.scss']
})
export class ActiveTimersComponent implements OnInit {
  timers!: any[];
  listLoading: boolean = true;

  constructor(private router: Router,
    private authService: AuthService,
    private timerService: TimerService,
    private toastr: ToastrService) { }

  async ngOnInit(): Promise<void> {
    await this.timerService.calculateUserActiveTimersPeriods(this.authService.loggedUserData.id!);
    this.timers = await this.timerService.getUserActiveTimers(this.authService.loggedUserData.id!);
    this.listLoading = false;
  }

  async finishTimer(timer: TimerModel): Promise<void> {
    var index = this.timers.indexOf(timer);
    this.timers.splice(index, 1);
    await this.timerService.finishTimer(timer.id!);

    this.toastr.success('The timer has been terminated');
  }
}
