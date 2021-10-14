import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimerSplitsListComponent } from './timer-splits-list.component';

describe('TimerSplitsListComponent', () => {
  let component: TimerSplitsListComponent;
  let fixture: ComponentFixture<TimerSplitsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimerSplitsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TimerSplitsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
