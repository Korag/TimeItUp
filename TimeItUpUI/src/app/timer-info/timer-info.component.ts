import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { RemoveTimerModalComponent } from '../remove-timer-modal';
import { TimerModel } from '../_models';
import { AuthService, TimerService, ValidationErrorPopulatorService } from '../_services';

@Component({
  selector: 'app-timer-info',
  templateUrl: './timer-info.component.html',
  styleUrls: ['./timer-info.component.scss']
})
export class TimerInfoComponent implements OnInit {
  @Input() timer!: TimerModel;
  updateTimerForm!: FormGroup;
  loading = false;
  formBlocked = true;
  submitted = false;
  reqErrors: any[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private router: Router,
    private timerService: TimerService,
    private authService: AuthService,
    private toastr: ToastrService,
    private validHelp: ValidationErrorPopulatorService) {
  }

  ngOnInit(): void {
    this.updateTimerForm = this.formBuilder.group({
      name: [{ value: this.timer.name, disabled: false }, Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(100)])],
      description: [{ value: this.timer.description, disabled: false }, Validators.compose([Validators.minLength(2), Validators.maxLength(255)])],
    })
  }

  public get f() { return this.updateTimerForm.controls; }

  async onSubmit() {
    this.submitted = true;

    if (this.updateTimerForm.invalid) {
      this.toastr.error('The form contains incorrectly entered data');
      return;
    }

    this.reqErrors = [];
    this.formBlocked = true;
    this.loading = true;

    if ((this.f.name.value !== this.timer.name)
      || (this.f.description.value !== this.timer.description)) {

      try {
        await this.timerService.updateTimerData(this.timer.id!,
          this.f.name.value, this.f.description.value);

        this.toastr.success('Updated timer data');

      } catch (err) {
        this.reqErrors = await this.validHelp.populateValidationErrorArray(err, this.reqErrors);

        if (err.error.status === 404) {
          this.reqErrors.push("The indicated timer does not exist.");
          this.toastr.warning("Timer doesn't exist");
        }
      }
    }

    this.loading = false;
  }

  async removeTimer(): Promise<void> {
    await this.timerService.removeTimer(this.timer.id!);
    this.toastr.success('The timer has been removed');
    this.router.navigate(["timers/active"]);
  }

  openRemoveTimerModal() {

    const modalRef = this.modalService.open(RemoveTimerModalComponent,
      {
        scrollable: true,
        windowClass: 'myCustomModalClass',
        keyboard: false,
        backdrop: 'static',
        centered: false,
        size: "modal-lg"
      });
    modalRef.componentInstance.deleteTimerEvent.subscribe(($e: any) => {
      this.removeTimer();
    });
  }
}
