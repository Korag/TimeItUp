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
        createdTimer.id = result.id;
        createdTimer.name = result.name;
        createdTimer.description = result.description;
        createdTimer.startAt = result.startAt;
        createdTimer.endAt = result.endAt;
        createdTimer.totalDuration = result.totalDuration;
        createdTimer.totalPausedTime = result.totalPausedTime;
        createdTimer.totalCountdownTimer = result.totalCountdownTimer;
        createdTimer.paused = result.paused;
        createdTimer.finished = result.finished;
        createdTimer.splitsNumber = result.splitsNumber;
        createdTimer.alarmsNumber = result.alarmsNumber;
        createdTimer.pausesNumber = result.pausesNumber;
      })).toPromise();

    return await createdTimer;
  }
}
