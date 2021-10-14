import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimerDurationSectionComponent } from './timer-duration-section.component';

describe('TimerDurationSectionComponent', () => {
  let component: TimerDurationSectionComponent;
  let fixture: ComponentFixture<TimerDurationSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimerDurationSectionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TimerDurationSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
