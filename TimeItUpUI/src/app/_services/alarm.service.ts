import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { AlarmModel } from '../_models';

@Injectable({
  providedIn: 'root'
})
export class AlarmService {

  constructor(private http: HttpClient) { }

  public async createAlarm(timerId: number, name: string, description: string, activationTime: Date): Promise<AlarmModel> {
    var createdAlarm = new AlarmModel();

    await this.http.post<AlarmModel>(`${environment.apiUrl}/Alarms`, { timerId, name, description, activationTime })
      .pipe(map(result => {
        createdAlarm = result;
      })).toPromise();

    return await createdAlarm;
  }

  public async updateAlarmData(id: number, name: string, description: string, activationTime: Date): Promise<void> {
    await this.http.put(`${environment.apiUrl}/Alarms/${id}`, { id, name, description, activationTime })
      .toPromise();
  }

  public async removeAlarm(id: number): Promise<void> {
    await this.http.delete(`${environment.apiUrl}/Alarms/${id}`, {})
      .toPromise();
  }

  public async getAlarmById(id: number): Promise<AlarmModel> {
    var alarm = new AlarmModel();

    await this.http.get<AlarmModel>(`${environment.apiUrl}/Alarms/${id}`, {})
      .pipe(map(result => {
        alarm = result;
      })).toPromise();

    return await alarm;
  }

  public async getUserActiveAlarms(userId: string): Promise<AlarmModel[]> {
    var activeAlarms: AlarmModel[] = [];

    await this.http.get<AlarmModel[]>(`${environment.apiUrl}/Alarms/Active/User/${userId}`, {})
      .pipe(map(result => {
        activeAlarms = result;
      })).toPromise();

    return await activeAlarms;
  }

  public async getTimerAlarms(timerId: number): Promise<AlarmModel[]> {
    var timerAlarms: AlarmModel[] = [];

    await this.http.get<AlarmModel[]>(`${environment.apiUrl}/Alarms/Timer/${timerId}`, {})
      .pipe(map(result => {
        timerAlarms = result;
      })).toPromise();

    return await timerAlarms;
  }
}
