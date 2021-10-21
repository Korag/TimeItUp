import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService, UserService } from '../_services';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.scss']
})
export class UserDetailsComponent implements OnInit {
  changeUserDataForm!: FormGroup;
  loading = false;
  formBlocked = true;
  submitted = false;
  reqErrors: any[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private authService: AuthService,
    private toastr: ToastrService) {
  }

  ngOnInit(): void {
    var loggedUserData = this.authService.loggedUserData;

    this.changeUserDataForm = this.formBuilder.group({
      email: [{ value: loggedUserData.email, disabled: false}, Validators.compose([Validators.required, Validators.email])],
      firstName: [{ value: loggedUserData.firstName, disabled: false }, Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(40)])],
      lastName: [{ value: loggedUserData.lastName, disabled: false }, Validators.compose([Validators.required, , Validators.minLength(4), Validators.maxLength(40)])],
    })
  }

  public get f() { return this.changeUserDataForm.controls; }

  async onSubmit() {
    this.submitted = true;

    if (this.changeUserDataForm.invalid) {
      this.toastr.error('The form contains incorrectly entered data');
      return;
    }

    this.reqErrors = [];
    this.formBlocked = true;
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

      let validationErrorDictionary = err.error.errors;

      if (err.error.errors !== null) {
        for (var fieldName in err.error.errors) {
          if (!this.reqErrors.hasOwnProperty(fieldName)) {
            this.reqErrors.push(validationErrorDictionary[fieldName]);
          }
        }

        this.toastr.warning('The form contains incorrectly entered data');
      }

      if (err.error.status === 409) {
        this.reqErrors.push("The user account associated with the email address entered already exists in the system.");
        this.toastr.warning("An existing account in the system is linked to the email address you enter");
      }
    }

    this.loading = false;
  }
}
