export class AuthTokenModel {

  constructor(public emailAddress: string, public jwt: string) {
    this.emailAddress = emailAddress;
    this.jwt = jwt;
  }
}
