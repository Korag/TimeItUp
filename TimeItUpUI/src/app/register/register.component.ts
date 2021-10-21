import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MustMatch } from '../_helpers';
import { AuthService, ValidationErrorPopulatorService } from '../_services';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  loading = false;
  submitted = false;
  reqErrors: any[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private toastr: ToastrService,
    private validHelp: ValidationErrorPopulatorService) {
  }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      email: ['', Validators.compose([Validators.required, Validators.email])],
      firstName: ['', Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(40)])],
      lastName: ['', Validators.compose([Validators.required, , Validators.minLength(4), Validators.maxLength(40)])],

      password: ['', Validators.compose([Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)])],
      confirmPassword: ['', Validators.compose([Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)])],
    },
      { validator: MustMatch('password', 'confirmPassword') });
  }

  public get f() { return this.registerForm.controls; }

  async onSubmit() {
    this.submitted = true;

    if (this.registerForm.invalid) {
      this.toastr.error('The form contains incorrectly entered data');
      return;
    }

    this.reqErrors = [];
    this.loading = true;

    try {
      var userAccountCreated = await this.authService.register(this.f.email.value, this.f.firstName.value,
        this.f.lastName.value, this.f.password.value,
        this.f.confirmPassword.value);

      if (userAccountCreated) {
        this.toastr.success('Your new user account has been created');
        this.router.navigate(["/login"]);
      }
    } catch (err) {
      this.reqErrors = await this.validHelp.populateValidationErrorArray(err, this.reqErrors);

      if (err.error.status === 409) {
        this.reqErrors.push("The user account associated with the email address entered already exists in the system.");
        this.toastr.warning("An existing account in the system is linked to the email address you enter");
      }
    }

    this.loading = false;
  }
}
