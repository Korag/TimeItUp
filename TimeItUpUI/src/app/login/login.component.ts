import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string = "";
  reqErrors: any[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private toastr: ToastrService) {
  }


  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['wojownik@poczta.onet.eu', Validators.compose([Validators.required, Validators.email])],
      password: ['Qwer!234', Validators.compose([Validators.required, Validators.minLength(6)])]
    });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  public get f() { return this.loginForm.controls; }

  async onSubmit() {
    this.submitted = true;

    if (this.loginForm.invalid) {
      this.toastr.error('The form contains incorrectly entered data');
      return;
    }

    this.loading = true;
    this.reqErrors = [];
    var loggedUser = null;

    try {
      loggedUser = await this.authService.login(this.f.email.value, this.f.password.value);
      if (loggedUser) {
        this.toastr.success('You have logged into your account');
        this.router.navigate(["/timers/active"]);
      }
    } catch (err) {
      this.reqErrors.push("");

      let validationErrorDictionary = err.error.errors;
      console.log(err);

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
