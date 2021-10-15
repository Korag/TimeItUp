export class UserModel {

  constructor(public id: string, public email: string,
    public firstName: string, public lastName: string)
  {
    this.id = id;
    this.email = email;
    this.firstName = firstName;
    this.lastName = lastName;
  }
}
