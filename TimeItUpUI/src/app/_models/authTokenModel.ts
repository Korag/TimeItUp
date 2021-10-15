export class AuthTokenModel {

  constructor(public email: string, public token: string) {
    this.email = email;
    this.token = token;
  }
}
