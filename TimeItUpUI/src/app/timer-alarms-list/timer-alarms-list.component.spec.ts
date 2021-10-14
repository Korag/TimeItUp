import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimerAlarmsListComponent } from './timer-alarms-list.component';

describe('TimerAlarmsListComponent', () => {
  let component: TimerAlarmsListComponent;
  let fixture: ComponentFixture<TimerAlarmsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimerAlarmsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TimerAlarmsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
