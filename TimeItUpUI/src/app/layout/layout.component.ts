import { HostListener } from '@angular/core';
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
  @HostListener('addUpdateRemoveAlarmEvent', ['$event'])
  async onCustomEventCaptured(event: any) {

    await clearInterval(this.intervalId);
    for (var i = 0; i < this.timeouts.length; i++) {
      clearTimeout(this.timeouts[i]);
    }
    this.timeouts = [];
    await this.ngOnInit();
  }

  intervalId!: any;
  timeouts: any[] = [];
  userActiveAlarms: AlarmModel[] = [];

  constructor(private alarmService: AlarmService,
    private authService: AuthService,
    private modalService: NgbModal) { }

  async ngOnInit(): Promise<void> {
    this.checkUserAlarms();
    this.intervalId = await setInterval(this.checkUserAlarms.bind(this), 60000);
  }

  async checkUserAlarms() {
    this.userActiveAlarms = await this.alarmService.getUserActiveAlarms(this.authService.loggedUserData.id!);
    const currentTime = new Date();

    for (let activeAlarm of this.userActiveAlarms) {
      let alarmActTime = new Date(activeAlarm.activationTime! + "Z")

      if ((alarmActTime.getTime() - currentTime.getTime()) < 60000) {
        this.timeouts.push(setTimeout(() => { this.displayAlarm(activeAlarm) }, alarmActTime.getTime() - currentTime.getTime()));
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
