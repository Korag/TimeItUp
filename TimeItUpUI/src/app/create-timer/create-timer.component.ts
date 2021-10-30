import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService, TimerService, UserService, ValidationErrorPopulatorService } from '../_services';

@Component({
  selector: 'app-create-timer',
  templateUrl: './create-timer.component.html',
  styleUrls: ['./create-timer.component.scss']
})
export class CreateTimerComponent implements OnInit {
  createTimerForm!: FormGroup;
  loading = false;
  submitted = false;
  reqErrors: any[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private timerService: TimerService,
    private authService: AuthService,
    private toastr: ToastrService,
    private validHelp: ValidationErrorPopulatorService) {
  }

  ngOnInit(): void {
    this.createTimerForm = this.formBuilder.group({
      name: [{ value: "", disabled: false }, Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(100)])],
      description: [{ value: "", disabled: false }, Validators.compose([Validators.maxLength(255)])],
    })
  }

  public get f() { return this.createTimerForm.controls; }

  async onSubmit() {
    this.submitted = true;

    if (this.createTimerForm.invalid) {
      this.toastr.error('The form contains incorrectly entered data');
      return;
    }

    this.reqErrors = [];
    this.loading = true;

    try {
      var createdTimer = await this.timerService.createTimer(this.authService.loggedUserData.id!, this.f.name.value, this.f.description.value);

      if (createdTimer) {
        this.toastr.success('Your new timer has been created');
        this.router.navigate(["/timers/active"]);
      }
    } catch (err) {
      console.log(err);
      this.reqErrors = await this.validHelp.populateValidationErrorArray(err, this.reqErrors);

      if (err.error.status === 409) {
        this.reqErrors.push("The user account associated with the email address entered already exists in the system.");
        this.toastr.warning("An existing account in the system is linked to the email address you enter");
      }

      if (err.error.status === 404) {
        this.reqErrors.push("The indicated user account does not exist.");
        this.toastr.warning("User account doesn't exist");
      }
    }

    this.loading = false;
  }
}
