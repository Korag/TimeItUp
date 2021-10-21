import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService, ValidationErrorPopulatorService } from '../_services';

@Component({
  selector: 'app-account-password-reset-get-token',
  templateUrl: './account-password-reset-get-token.component.html',
  styleUrls: ['./account-password-reset-get-token.component.scss']
})
export class AccountPasswordResetGetTokenComponent implements OnInit {
  getResetPasswordTokenForm!: FormGroup;
  loading = false;
  submitted = false;
  reqErrors: any[] = [];
  info: string = "";
  getTokenButtonBlocked: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private toastr: ToastrService,
    private validHelp: ValidationErrorPopulatorService) {
  }

  ngOnInit(): void {
    this.getResetPasswordTokenForm = this.formBuilder.group({
      email: ['', Validators.compose([Validators.required, Validators.email])],
    });
  }

  public get f() { return this.getResetPasswordTokenForm.controls; }

  async onSubmit() {
    this.submitted = true;

    if (this.getResetPasswordTokenForm.invalid) {
      this.toastr.error('The form contains incorrectly entered data');
      return;
    }

    this.reqErrors = [];
    this.getTokenButtonBlocked = true;
    this.loading = true;

    try {
      await this.userService.getResetPasswordToken(this.f.email.value);
      this.toastr.info('A link to the actual password reset action has been sent to your email address');
      this.info = "Guidelines have been provided to your email address to reset your account password.";
    } catch (err) {
      this.reqErrors = await this.validHelp.populateValidationErrorArray(err, this.reqErrors);

      if (err.error.status === 404) {
        this.reqErrors.push("No user with the specified email address and password was found.");
        this.toastr.warning("Specific user account doesn't exist");
      }

      this.getTokenButtonBlocked = false;
    }

    this.loading = false;
  }
}

