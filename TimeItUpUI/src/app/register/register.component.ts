import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MustMatch } from '../_helpers';
import { AuthService } from '../_services';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  loading = false;
  submitted = false;
  error = '';

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService) {
  }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      email: ['', Validators.compose([Validators.required, Validators.email])],
      firstName: ['', Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(40)])],
      lastName: ['', Validators.compose([Validators.required, , Validators.minLength(4), Validators.maxLength(40)])],

      password: ['', Validators.compose([Validators.required, Validators.pattern(`^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$`)])],
      confirmPassword: ['', Validators.compose([Validators.required, Validators.pattern(`^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$`)])],
    },
      { validator: MustMatch('password', 'confirmPassword') });
  }

  public get f() { return this.registerForm.controls; }

  async onSubmit() {
    this.submitted = true;

    if (this.registerForm.invalid) {
      return;
    }

    this.loading = true;
    var userAccountCreated = await this.authService.register(this.f.email.value, this.f.firstName.value,
      this.f.lastName.value, this.f.password.value,
      this.f.confirmPassword.value);

    if (userAccountCreated) {
      this.router.navigate(["/login"]);
    }
    else {
      this.error = "Incorrect register data";
      this.loading = false;
    }
  }
}
