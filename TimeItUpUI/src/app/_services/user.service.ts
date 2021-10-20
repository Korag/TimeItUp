import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) {
  }

  public async getResetPasswordToken(email: string): Promise<void> {
    await this.http.post(`${environment.apiUrl}/Accounts/TryResetPassword/${email}`, {})
      .toPromise();
  }

  public async resetPassword(email: string, token: string, password: string, confirmPassword: string): Promise<void> {
    await this.http.put(`${environment.apiUrl}/Accounts/ResetPassword`, { email, token, password, confirmPassword })
      .toPromise();
  }
}
