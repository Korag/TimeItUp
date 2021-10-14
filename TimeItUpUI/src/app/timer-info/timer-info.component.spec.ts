import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimerInfoComponent } from './timer-info.component';

describe('TimerInfoComponent', () => {
  let component: TimerInfoComponent;
  let fixture: ComponentFixture<TimerInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimerInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TimerInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
