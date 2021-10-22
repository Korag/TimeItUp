export class TimerModel {
  id?: string;
  name?: string;
  description?: string;
  startAt?: Date;
  endAt?: Date;
  totalDuration?: string;
  totalPausedTime?: string;
  totalCountdownTimer?: string;
  paused?: boolean;
  finished?: boolean;
  splitsNumber?: number;
  alarmsNumber?: number;
  pausesNumber?: number;
}
