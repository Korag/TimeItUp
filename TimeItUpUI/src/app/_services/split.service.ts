import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { SplitModel } from '../_models';

@Injectable({
  providedIn: 'root'
})
export class SplitService {

  constructor(private http: HttpClient) {
  }

  public async createSplit(timerId: number): Promise<SplitModel> {
    var createdSplit = new SplitModel();

    await this.http.post<SplitModel>(`${environment.apiUrl}/Splits`, { timerId })
      .pipe(map(result => {
        createdSplit = result;
      })).toPromise();

    return await createdSplit;
  }

  public async finishSplit(id: number): Promise<void> {
    await this.http.put(`${environment.apiUrl}/Splits/Finish/${id}`, {}).toPromise();
  }

  public async startSplit(id: number): Promise<void> {
    await this.http.put(`${environment.apiUrl}/Splits/Start/${id}`, {}).toPromise();
  }

  public async getTimerActiveSplit(timerId: number): Promise<SplitModel> {
    var activeSplit = new SplitModel();

    await this.http.get<SplitModel>(`${environment.apiUrl}/Splits/Active/Timer/${timerId}`, {})
      .pipe(map(result => {
        activeSplit = result;
      })).toPromise();

    return await activeSplit;
  }

  public async getTimerSplits(timerId: number): Promise<SplitModel[]> {
    var timerSplits: SplitModel[] = [];

    await this.http.get<SplitModel[]>(`${environment.apiUrl}/Splits/Timer/${timerId}`, {})
      .pipe(map(result => {
        timerSplits = result;
      })).toPromise();

    return await timerSplits;
  }
}
