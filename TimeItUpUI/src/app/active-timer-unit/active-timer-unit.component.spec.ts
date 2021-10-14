import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActiveTimerUnitComponent } from './active-timer-unit.component';

describe('ActiveTimerUnitComponent', () => {
  let component: ActiveTimerUnitComponent;
  let fixture: ComponentFixture<ActiveTimerUnitComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ActiveTimerUnitComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ActiveTimerUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
