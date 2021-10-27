import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TimerModel } from '../_models';
import { TimerService } from '../_services';

@Component({
  selector: 'app-timer-details',
  templateUrl: './timer-details.component.html',
  styleUrls: ['./timer-details.component.scss']
})
export class TimerDetailsComponent implements OnInit {
  timer!: TimerModel;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private timerService: TimerService,
    private toastr: ToastrService) { }

  async ngOnInit(): Promise<void> {
    this.timer = await this.timerService.getTimerById(this.route.snapshot.params.id);
  }

  async finishTimer(timer: TimerModel): Promise<void> {
    await this.timerService.finishTimer(timer.id!);
    this.toastr.success('The timer has been terminated');
    this.router.navigate(["timers/active"]);
  }
}
