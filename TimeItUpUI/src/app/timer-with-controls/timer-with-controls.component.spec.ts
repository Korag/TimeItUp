import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimerWithControlsComponent } from './timer-with-controls.component';

describe('TimerWithControlsComponent', () => {
  let component: TimerWithControlsComponent;
  let fixture: ComponentFixture<TimerWithControlsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimerWithControlsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TimerWithControlsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
