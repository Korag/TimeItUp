import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CreateAlarmModalComponent } from '../create-alarm-modal';
import { RemoveAlarmModalComponent } from '../remove-alarm-modal';
import { UpdateAlarmModalComponent } from '../update-alarm-modal';
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

  constructor(
    private alarmService: AlarmService,
    private modalService: NgbModal,
    private toastr: ToastrService) { }

  async ngOnInit(): Promise<void> {
    this.alarms = await this.alarmService.getTimerAlarms(this.timer.id!);
    this.listLoading = false;
  }

  openUpdateAlarmModal(alarm: AlarmModel) {
    const modalRef = this.modalService.open(UpdateAlarmModalComponent,
      {
        scrollable: true,
        windowClass: 'myCustomModalClass',
        keyboard: false,
        backdrop: 'static',
        centered: false,
        size: "modal-lg"
      });
    modalRef.componentInstance.alarm = alarm;
    modalRef.componentInstance.updateAlarmEvent.subscribe(($e: any) => {
      this.updateAlarm();
    });
  }

  openRemoveAlarmModal(alarm: AlarmModel) {
    const modalRef = this.modalService.open(RemoveAlarmModalComponent,
      {
        scrollable: true,
        windowClass: 'myCustomModalClass',
        keyboard: false,
        backdrop: 'static',
        centered: false,
        size: "modal-lg"
      });
    modalRef.componentInstance.alarm = alarm;
    modalRef.componentInstance.removeAlarmEvent.subscribe(($e: any) => {
      this.removeAlarm($e);
    });
  }

  openAddAlarmModal() {
    const modalRef = this.modalService.open(CreateAlarmModalComponent,
      {
        scrollable: true,
        windowClass: 'myCustomModalClass',
        keyboard: false,
        backdrop: 'static',
        centered: false,
        size: "modal-lg"
      });
    modalRef.componentInstance.timer = this.timer;
    modalRef.componentInstance.createAlarmEvent.subscribe(($e: any) => {
      this.addAlarm();
    });
  }

  async removeAlarm(alarm: AlarmModel) {
    //var index = this.alarms.indexOf(alarm);
    //this.alarms.splice(index, 1);
    await this.alarmService.removeAlarm(alarm.id!);

    this.toastr.success('The alarm has been removed');
    this.listLoading = false;
    await this.ngOnInit();
  }

  async updateAlarm() {
    this.listLoading = false;
    await this.ngOnInit();
  }

  async addAlarm() {
    this.listLoading = false;
    await this.ngOnInit();
  }
}

