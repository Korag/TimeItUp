import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { AlarmModel, TimerModel } from '../_models';
import { AlarmService, ValidationErrorPopulatorService } from '../_services';

@Component({
  selector: 'app-create-alarm-modal',
  templateUrl: './create-alarm-modal.component.html',
  styleUrls: ['./create-alarm-modal.component.scss']
})
export class CreateAlarmModalComponent implements OnInit {
  @Input() timer!: TimerModel;
  @Output() createAlarmEvent = new EventEmitter<any>();

  createAlarmForm!: FormGroup;
  loading = false;
  submitted = false;
  reqErrors: any[] = [];

  alarmActivationTime?: Date = new Date();

  constructor(private modalService: NgbModal,
    private alarmService: AlarmService,
    public activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private validHelp: ValidationErrorPopulatorService) { }

  async ngOnInit(): Promise<void> {
    this.createAlarmForm = this.formBuilder.group({
      name: [{ value: "", disabled: false }, Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(100)])],
      description: [{ value: "", disabled: false }, Validators.compose([Validators.maxLength(255)])],
      alarmActivationTime: [{ value: this.alarmActivationTime, disabled: false }],
    })
  }

  public get f() { return this.createAlarmForm.controls; }

  async closeModal() {
    this.activeModal.close();
  }

  async createAlarm() {
    this.submitted = true;

    if (this.createAlarmForm.invalid) {
      this.toastr.error('The form contains incorrectly entered data');
      return;
    }

    this.reqErrors = [];
    this.loading = true;

    try {
      const activationTimeLocalDate = new Date(this.f.alarmActivationTime.value);
      var createdAlarm = await this.alarmService.createAlarm(this.timer.id!, this.f.name.value, this.f.description.value, activationTimeLocalDate);

      if (createdAlarm) {
        this.toastr.success('New alarm has been created');
      }
    } catch (err) {
      this.reqErrors = await this.validHelp.populateValidationErrorArray(err, this.reqErrors);

      if (err.error.status === 404) {
        this.reqErrors.push("The indicated timer does not exist.");
        this.toastr.warning("Timer doesn't exist");
      }

      this.closeModal();
    }

    this.loading = false;

    this.createAlarmEvent.emit();
    this.closeModal();
  }
}
