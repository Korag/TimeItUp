export class AuthorizedUserModel {

  constructor(public id: string, public email: string, public firstName: string,
              public lastName: string, public token: string

  ) {
    this.id = id;
    this.email = email;
    this.firstName = firstName;
    this.lastName = lastName;
    this.token = token;
  }
}
