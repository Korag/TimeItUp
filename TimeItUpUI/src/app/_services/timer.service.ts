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

  public async getTimerById(id: number): Promise<TimerModel> {
    var timer = new TimerModel();

    await this.http.get<TimerModel>(`${environment.apiUrl}/Timers/${id}`, {})
      .pipe(map(result => {
        timer = result;
      })).toPromise();

    return await timer;
  }

  public async updateTimerData(id: number, name: string, description: string): Promise<void> {
    await this.http.put(`${environment.apiUrl}/Timers/${id}`, { id, name, description })
      .toPromise();
  }

  public async removeTimer(id: number): Promise<void> {
    await this.http.delete(`${environment.apiUrl}/Timers/${id}`, { })
      .toPromise();
  }

  public async finishTimer(id: number): Promise<void> {
    await this.http.put(`${environment.apiUrl}/Timers/Finish/${id}`, {}).toPromise();
  }

  public async startTimer(id: number): Promise<void> {
    await this.http.put(`${environment.apiUrl}/Timers/Start/${id}`, {}).toPromise();
  }

  public async reinstateTimer(id: number): Promise<void> {
    await this.http.put(`${environment.apiUrl}/Timers/Reinstate/${id}`, {}).toPromise();
  }

  public async getUserActiveTimers(userId: string): Promise<TimerModel[]> {
    var activeTimers: TimerModel[] = [];

    await this.http.get<TimerModel[]>(`${environment.apiUrl}/Timers/Active/User/${userId}`, {})
      .pipe(map(result => {
        activeTimers = result;
      })).toPromise();

    return await activeTimers;
  }

  public async getUserPastTimers(id: string): Promise<TimerModel[]> {
    var pastTimers: TimerModel[] = [];

    await this.http.get<TimerModel[]>(`${environment.apiUrl}/Timers/Finished/User/${id}`, {})
      .pipe(map(result => {
        pastTimers = result;
      })).toPromise();

    return await pastTimers;
  }

  public async calculateSelectedTimerPeriods(id: number): Promise<void> {

    await this.http.put(`${environment.apiUrl}/Timers/CalculatePeriods/${id}`, {}).toPromise();
  }

  public async calculateUserActiveTimersPeriods(id: string): Promise<void> {

    await this.http.put(`${environment.apiUrl}/Timers/Active/User/CalculatePeriods/${id}`, {}).toPromise();
  }
}
