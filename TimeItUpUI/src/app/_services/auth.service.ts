import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { JwtHelperService } from "@auth0/angular-jwt";

import { AuthorizedUserModel, AuthTokenModel, UserModel } from '../_models';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedUser: AuthorizedUserModel;

  constructor(private http: HttpClient) {
    this.loggedUser = this.getUserDataFromLocalStorage();
    console.log(this.loggedUser);

    const jwtHelper = new JwtHelperService();

    if (jwtHelper.isTokenExpired(this.loggedUser?.token)) {
      this.logout();
    }
  }

  private getUserDataFromLocalStorage(): AuthorizedUserModel {
    var localStorageItem = localStorage.getItem('storedUserData');
    return JSON.parse(localStorageItem!);
  }

  public get loggedUserData(): AuthorizedUserModel {
    this.loggedUser = this.getUserDataFromLocalStorage();

    const jwtHelper = new JwtHelperService();

    if (jwtHelper.isTokenExpired(this.loggedUser?.token)) {
      this.logout();
    }

    return this.loggedUser;
  }

  public async login(email: string, password: string): Promise<AuthorizedUserModel> {
    await this.http.post<AuthTokenModel>(`${environment.apiUrl}/Accounts/login`, { email, password })
      .pipe(map(result => {
        this.loggedUser = new AuthorizedUserModel();
        this.loggedUser.email = result.emailAddress;
        this.loggedUser.token = result.jwt;
        localStorage.setItem('storedUserData', JSON.stringify(this.loggedUser));
      })).toPromise();

    await this.http.get<UserModel>(`${environment.apiUrl}/Users/Email/${this.loggedUser.email}`)
      .pipe(map(result => {
        this.loggedUser.id = result.id;
        this.loggedUser.firstName = result.firstName;
        this.loggedUser.lastName = result.lastName;

        localStorage.setItem('storedUserData', JSON.stringify(this.loggedUser));
      })).toPromise();

    return await this.loggedUser;
  }

  public async register(email: string, firstName: string, lastName: string,
    password: string, confirmPassword: string): Promise<boolean> {

    var userCreated = false;
    await this.http.post<UserModel>(`${environment.apiUrl}/Accounts/register`,
      { email, firstName, lastName, password, confirmPassword })
      .pipe(map(result => {
        if (result.id !== null) {
          userCreated = true;
        }
      })).toPromise();

    return await userCreated;
  }

  public logout() {
    this.loggedUser = null!;
    localStorage.removeItem('storedUserData');
  }
}
