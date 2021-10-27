import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment.prod';
import { PauseModel } from '../_models';

@Injectable({
  providedIn: 'root'
})
export class PauseService {

  constructor(private http: HttpClient) {
  }

  public async createPause(timerId: number): Promise<PauseModel> {
    var createdPause = new PauseModel();

    await this.http.post<PauseModel>(`${environment.apiUrl}/Pauses`, { timerId })
      .pipe(map(result => {
        createdPause = result;
      })).toPromise();

    return await createdPause;
  }

  public async finishPause(id: number): Promise<void> {
    await this.http.put(`${environment.apiUrl}/Pauses/Finish/${id}`, {}).toPromise();
  }

  public async startPause(id: number): Promise<void> {
    await this.http.put(`${environment.apiUrl}/Pauses/Start/${id}`, {}).toPromise();
  }

  public async getTimerActivePause(timerId: number): Promise<PauseModel> {
    var activePause = new PauseModel();

    await this.http.get<PauseModel>(`${environment.apiUrl}/Pauses/Active/Timer/${timerId}`, {})
      .pipe(map(result => {
        activePause = result;
      })).toPromise();

    return await activePause;
  }

  public async getTimerPauses(timerId: number): Promise<PauseModel[]> {
    var timerPauses: PauseModel[] = [];

    await this.http.get<PauseModel[]>(`${environment.apiUrl}/Pauses/Timer/${timerId}`, {})
      .pipe(map(result => {
        timerPauses = result;
      })).toPromise();

    return await timerPauses;
  }
}
