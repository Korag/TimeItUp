import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../_services';

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
    private userService: UserService) {
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
      return;
    }

    this.reqErrors = [];
    this.getTokenButtonBlocked = true;
    this.loading = true;

    try {
      await this.userService.getResetPasswordToken(this.f.email.value);
      this.info = "Guidelines have been provided to your email address to reset your account password.";
    } catch (err) {

      let validationErrorDictionary = err.error.errors;

      if (err.error.errors !== null) {
        for (var fieldName in err.error.errors) {
          if (!this.reqErrors.hasOwnProperty(fieldName)) {
            this.reqErrors.push(validationErrorDictionary[fieldName]);
          }
        }
      }

      if (err.error.status === 404) {
        this.reqErrors.push("No user with the specified email address and password was found.");
      }

      this.getTokenButtonBlocked = false;
    }

    this.loading = false;
  }
}

