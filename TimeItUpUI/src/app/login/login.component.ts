import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService, ValidationErrorPopulatorService } from '../_services';

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
    private toastr: ToastrService,
    private validHelp: ValidationErrorPopulatorService) {
  }


  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.compose([Validators.required, Validators.minLength(6)])]
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
      this.reqErrors = await this.validHelp.populateValidationErrorArray(err, this.reqErrors);

      if (err.error.status === 404) {
        this.reqErrors.push("No user with the specified email address and password was found.");
        this.toastr.warning("Specific user account doesn't exist");
      }
    }

    this.loading = false;
  }
}
