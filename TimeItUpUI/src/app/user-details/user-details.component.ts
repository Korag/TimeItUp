import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthorizedUserModel } from '../_models';
import { AuthService, UserService, ValidationErrorPopulatorService } from '../_services';

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
  changeMade: boolean = false;
  loggedUserData: AuthorizedUserModel = new AuthorizedUserModel();

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private authService: AuthService,
    private toastr: ToastrService,
    private validHelp: ValidationErrorPopulatorService) {
  }

  ngOnInit(): void {
    this.loggedUserData = this.authService.loggedUserData;

    this.changeUserDataForm = this.formBuilder.group({
      email: [{ value: this.loggedUserData.email, disabled: false }, Validators.compose([Validators.required, Validators.email])],
      firstName: [{ value: this.loggedUserData.firstName, disabled: false }, Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(40)])],
      lastName: [{ value: this.loggedUserData.lastName, disabled: false }, Validators.compose([Validators.required, , Validators.minLength(4), Validators.maxLength(40)])],
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

    if ((this.f.firstName.value !== this.loggedUserData.firstName)
      || (this.f.lastName.value !== this.loggedUserData.lastName)) {

      await this.updateUserData();
    }

    if (this.f.email.value !== this.loggedUserData.email) {

      await this.updateUserEmail();
    }

    if (this.changeMade) {
      this.authService.logout();
      this.router.navigate(["/login"]);
    }

    this.loading = false;
  }

  private async updateUserData() {
    try {
      await this.userService.updateUserData(this.loggedUserData.id!,
        this.f.firstName.value, this.f.lastName.value);

      this.toastr.success('Updated user data');
      this.changeMade = true;

    } catch (err) {
      this.reqErrors = await this.validHelp.populateValidationErrorArray(err, this.reqErrors);

      if (err.error.status === 404) {
        this.reqErrors.push("The indicated user account does not exist.");
        this.toastr.warning("User account doesn't exist");
      }
    }
  }

  private async updateUserEmail() {
    try {
      let newEmail = this.f.email.value;

      await this.userService.changeUserEmail(this.loggedUserData.email!,
        this.loggedUserData.id!, newEmail);

      this.toastr.success('Updated user email');
      this.changeMade = true;
    } catch (err) {
      this.reqErrors = await this.validHelp.populateValidationErrorArray(err, this.reqErrors);

      if (err.error.status === 404) {
        this.reqErrors.push("The indicated user account does not exist.");
      }
    }
  }
}
