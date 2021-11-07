import { Input } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { AlarmModel, TimerModel } from '../_models';
import { TimerService } from '../_services';

@Component({
  selector: 'app-active-alarm-notification-modal',
  templateUrl: './active-alarm-notification-modal.component.html',
  styleUrls: ['./active-alarm-notification-modal.component.scss']
})
export class ActiveAlarmNotificationModalComponent implements OnInit {
  @Input() alarm!: AlarmModel;
  timer!: TimerModel;
  loading: boolean = false;

  constructor(private timerService: TimerService,
              private activeModal: NgbActiveModal,
              private toastr: ToastrService) { }

  async ngOnInit(): Promise<void> {
    this.loading = true;
    this.timer = await this.timerService.getTimerById(this.alarm.timerId!);
    this.loading = false;
  }

  async closeModal() {
    this.activeModal.close();
    this.toastr.info('Alarm has been dismissed');
  }
}
