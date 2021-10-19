import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorizedUserModel } from '../_models';
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
    private authService: AuthService) {
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
      return;
    }

    this.loading = true;
    this.reqErrors = [];
    var loggedUser = null;

    try {
      loggedUser = await this.authService.login(this.f.email.value, this.f.password.value);
      if (loggedUser) {
        this.router.navigate(["/timers/active"]);
      }
    } catch (err) {

      let validationErrorDictionary = err.error.errors;

      for (var fieldName in err.error.errors) {
        if (!this.reqErrors.hasOwnProperty(fieldName)) {
          this.reqErrors.push(validationErrorDictionary[fieldName]);
        }
      }

      this.loading = false;
    }
  }
}
