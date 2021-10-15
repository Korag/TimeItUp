import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment.prod';
import { AuthorizedUserModel, AuthTokenModel, UserModel } from '../_models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedUser: AuthorizedUserModel;

  constructor(private http: HttpClient) {
    this.loggedUser = this.getUserDataFromLocalStorage();
  }

  private getUserDataFromLocalStorage(): AuthorizedUserModel{
    var localStorageItem = localStorage.getItem('storedUserData');
    return JSON.parse(localStorageItem!);
  }

  public get loggedUserData(): AuthorizedUserModel {
    return this.loggedUser;
  }

  public login(email: string, password: string) {
    this.http.post<AuthTokenModel>(`${environment.apiUrl}/Accounts/login`, { email, password })
      .pipe(map(result => {
        this.loggedUser.email = result.email;
        this.loggedUser.token = result.token;

        localStorage.setItem('storedUserData', JSON.stringify(this.loggedUser));
      }));

    this.http.get<UserModel>(`${environment.apiUrl}/Users/Email/${this.loggedUser.email}`)
      .pipe(map(result => {
        this.loggedUser.id = result.id;
        this.loggedUser.firstName = result.firstName;
        this.loggedUser.lastName = result.lastName;

        localStorage.setItem('storedUserData', JSON.stringify(this.loggedUser));
    }));
  }

  public logout() {
    localStorage.removeItem('storedUserData');
  }
}
