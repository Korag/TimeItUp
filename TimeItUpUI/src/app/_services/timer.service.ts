import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment.prod';
import { TimerModel } from '../_models';

@Injectable({
  providedIn: 'root'
})
export class TimerService {

  constructor(private http: HttpClient) {
  }

  public async createTimer(userId: string, name: string, description: string): Promise<TimerModel> {
    var createdTimer = new TimerModel();

    await this.http.post<TimerModel>(`${environment.apiUrl}/Timers`, { userId, name, description })
      .pipe(map(result => {
        createdTimer = result;
      })).toPromise();

    return await createdTimer;
  }

  public async finishTimer(id: number): Promise<void> {
    await this.http.put(`${environment.apiUrl}/Timers/Finish/${id}`, { }).toPromise();
  }

  public async startTimer(id: number): Promise<void> {
    await this.http.put(`${environment.apiUrl}/Timers/Start/${id}`, { }).toPromise();
  }

  public async getUserActiveTimers(id: string): Promise<TimerModel[]> {
    var activeTimers: TimerModel[] = [];

    await this.http.get<TimerModel[]>(`${environment.apiUrl}/Timers/Active/User/${id}`, {})
      .pipe(map(result => {
        activeTimers = result;
      })).toPromise();

    return await activeTimers;
  }

  public async calculateUserActiveTimersPeriods(id: string): Promise<void> {

    await this.http.put<TimerModel[]>(`${environment.apiUrl}/Timers/Active/User/CalculatePeriods/${id}`, {}).toPromise();
  }
}