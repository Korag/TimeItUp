import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimerPausesListComponent } from './timer-pauses-list.component';

describe('TimerPausesListComponent', () => {
  let component: TimerPausesListComponent;
  let fixture: ComponentFixture<TimerPausesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimerPausesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TimerPausesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
