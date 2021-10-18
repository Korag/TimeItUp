export class UserRegisterModel {

  constructor(public email: string, public firstName: string, public lastName: string,
              public password: string, public confirmPassword: string) {
    this.email = email;
    this.firstName = firstName;
    this.lastName = lastName;
    this.password = password;
    this.confirmPassword = confirmPassword;
  }
}
