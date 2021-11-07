import { Output } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { AlarmModel } from '../_models';
import { AlarmService, ValidationErrorPopulatorService } from '../_services';

@Component({
  selector: 'app-update-alarm-modal',
  templateUrl: './update-alarm-modal.component.html',
  styleUrls: ['./update-alarm-modal.component.scss']
})
export class UpdateAlarmModalComponent implements OnInit {
  @Input() alarm!: AlarmModel;
  @Output() updateAlarmEvent = new EventEmitter<any>();

  updateAlarmForm!: FormGroup;
  loading = false;
  submitted = false;
  reqErrors: any[] = [];

  constructor(private modalService: NgbModal,
    private alarmService: AlarmService,
    public activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private validHelp: ValidationErrorPopulatorService) { }

  async ngOnInit(): Promise<void> {
    this.updateAlarmForm = this.formBuilder.group({
      name: [{ value: this.alarm.name, disabled: false }, Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(100)])],
      description: [{ value: this.alarm.description, disabled: false }, Validators.compose([Validators.maxLength(255)])],
      alarmActivationTime: [{ value: this.alarm.activationTime, disabled: false }],
    })
  }

  public get f() { return this.updateAlarmForm.controls; }

  async closeModal() {
    this.activeModal.close();
  }

  async updateAlarm() {
    this.submitted = true;

    if (this.updateAlarmForm.invalid) {
      this.toastr.error('The form contains incorrectly entered data');
      return;
    }

    this.reqErrors = [];
    this.loading = true;

    try {
      const activationTimeLocalDate = new Date(this.f.alarmActivationTime.value);
      await this.alarmService.updateAlarmData(this.alarm.id!, this.f.name.value, this.f.description.value, activationTimeLocalDate);

      this.toastr.success('Selected alarm has been updated');
      this.updateAlarmEvent.emit();
      this.closeModal();

    } catch (err) {
      this.reqErrors = await this.validHelp.populateValidationErrorArray(err, this.reqErrors);

      if (err.error.status === 404) {
        this.reqErrors.push("The indicated alarm does not exist.");
        this.toastr.warning("Alarm doesn't exist");
      }
    }

    this.loading = false;
  }
}
