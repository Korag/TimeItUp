export class TimerModel {
  id?: number;
  name?: string;
  description?: string;
  startAt?: Date;
  endAt?: Date;
  totalDuration?: string;
  totalPausedTime?: string;
  totalCountdownTime?: string;
  paused?: boolean;
  finished?: boolean;
  splitsNumber?: number;
  alarmsNumber?: number;
  pausesNumber?: number;
}
