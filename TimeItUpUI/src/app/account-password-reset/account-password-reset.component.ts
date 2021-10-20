import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MustMatch } from '../_helpers';
import { UserService } from '../_services';

@Component({
  selector: 'app-account-password-reset',
  templateUrl: './account-password-reset.component.html',
  styleUrls: ['./account-password-reset.component.scss']
})
export class AccountPasswordResetComponent implements OnInit {
  resetPasswordForm!: FormGroup;
  loading = false;
  submitted = false;
  email = "";
  token = "";
  reqErrors: any[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private toastr: ToastrService) {
  }

  ngOnInit(): void {

    const routeParams = this.route.snapshot.paramMap;

    this.email = String(routeParams.get('email'));
    this.token = atob(String(routeParams.get('token')));

    this.resetPasswordForm = this.formBuilder.group({
      password: ['', Validators.compose([Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)])],
      confirmPassword: ['', Validators.compose([Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)])],
    },
      { validator: MustMatch('password', 'confirmPassword') });
  }

  public get f() { return this.resetPasswordForm.controls; }

  async onSubmit() {
    this.submitted = true;

    if (this.resetPasswordForm.invalid) {
      this.toastr.error('The form contains incorrectly entered data');
      return;
    }

    this.reqErrors = [];
    this.loading = true;

    try {
      console.log(this.f.password.value);
      console.log(this.f.confirmPassword.value);

       await this.userService.resetPassword(this.email, this.token,
         this.f.password.value, this.f.confirmPassword.value);

      this.toastr.success('Your user account password has been changed successfully');
        this.router.navigate(["/login"]);
    } catch (err) {
      console.log(err);

      let validationErrorDictionary = err.error.errors;

      if (err.error.errors !== null) {
        for (var fieldName in err.error.errors) {
          if (!this.reqErrors.hasOwnProperty(fieldName)) {
            this.reqErrors.push(validationErrorDictionary[fieldName]);
          }
        }

        this.toastr.warning('The form contains incorrectly entered data');
      }

      if (err.error.status === 404) {
        this.reqErrors.push("No user with the specified email address and password was found.");
        this.toastr.warning("Specific user account doesn't exist");
      }
    }

    this.loading = false;
  }
}
