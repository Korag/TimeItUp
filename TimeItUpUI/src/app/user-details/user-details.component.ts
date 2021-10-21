import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthorizedUserModel } from '../_models';
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
  changeMade: boolean = false;
  loggedUserData: AuthorizedUserModel = new AuthorizedUserModel();

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private authService: AuthService,
    private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.loggedUserData = this.authService.loggedUserData;

    this.changeUserDataForm = this.formBuilder.group({
      email: [{ value: this.loggedUserData.email, disabled: false}, Validators.compose([Validators.required, Validators.email])],
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

    console.log("jestem w funkcji");

    if ((this.f.firstName.value !== this.loggedUserData.firstName)
      || (this.f.lastName.value !== this.loggedUserData.lastName)) {

      console.log("wykryto zmiane danych osobowych");

      try {
        console.log("leci request z osobówkami");

        await this.userService.updateUserData(this.loggedUserData.id!,
          this.f.firstName.value, this.f.lastName.value);

        this.toastr.success('Updated user data');
        this.changeMade = true;

      } catch (err) {
        console.log("osobówki error");

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
          this.reqErrors.push("The indicated user account does not exist.");
        }
      }
    }
    console.log("jestem przed 2 warunkiem");

    if (this.f.email.value !== this.loggedUserData.email) {
      try {
        let newEmail = this.f.email.value;

        console.log("zmieniono emaila");

        await this.userService.changeUserEmail(this.loggedUserData.email!,
          this.loggedUserData.id!, newEmail);

        this.toastr.success('Updated user email');
        this.changeMade = true;
      } catch (err) {

        console.log("błąd dla emaila");

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
          this.reqErrors.push("The indicated user account does not exist.");
        }
      }
    }
    console.log("finish");

    if (this.changeMade) {
      this.authService.logout();
      this.router.navigate(["/login"]);
    }

    this.loading = false;
  }
}
