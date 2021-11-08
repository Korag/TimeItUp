import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AlarmModel } from '../_models';

@Component({
  selector: 'app-remove-alarm-modal',
  templateUrl: './remove-alarm-modal.component.html',
  styleUrls: ['./remove-alarm-modal.component.scss']
})
export class RemoveAlarmModalComponent implements OnInit {
  @Input() public alarm!: AlarmModel;
  @Output() removeAlarmEvent = new EventEmitter<AlarmModel>();

  constructor(
    public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  closeModal() {
    this.activeModal.close();
  }

  removeAlarm() {
    this.removeAlarmEvent.emit(this.alarm);
    this.closeModal();
  }
}
