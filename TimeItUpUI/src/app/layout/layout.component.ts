import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ActiveAlarmNotificationModalComponent } from '../active-alarm-notification-modal';
import { AlarmModel } from '../_models';
import { AlarmService, AuthService } from '../_services';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  intervalId!: any;
  userActiveAlarms: AlarmModel[] = [];

  constructor(private alarmService: AlarmService,
    private authService: AuthService,
    private modalService: NgbModal) { }

  async ngOnInit(): Promise<void> {
    this.intervalId = await setInterval(this.checkUserAlarms.bind(this), 60000);
  }

  async checkUserAlarms() {
    this.userActiveAlarms = await this.alarmService.getUserActiveAlarms(this.authService.loggedUserData.id!);
    const currentTime = new Date();

    for (let activeAlarm of this.userActiveAlarms) {
      if ((activeAlarm.activationTime!.getMinutes() - currentTime.getMinutes()) === 0) {
        setTimeout(() => { this.displayAlarm(activeAlarm) }, 10000);
      }
    }
  }

  async displayAlarm(alarm: AlarmModel) {
    const modalRef = this.modalService.open(ActiveAlarmNotificationModalComponent,
      {
        scrollable: true,
        windowClass: 'myCustomModalClass',
        keyboard: false,
        backdrop: 'static',
        centered: false,
        size: "modal-lg"
      });
    modalRef.componentInstance.alarm = alarm;
  }
}
