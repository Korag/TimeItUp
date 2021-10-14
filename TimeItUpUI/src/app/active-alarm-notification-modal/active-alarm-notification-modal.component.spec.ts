import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActiveAlarmNotificationModalComponent } from './active-alarm-notification-modal.component';

describe('ActiveAlarmNotificationModalComponent', () => {
  let component: ActiveAlarmNotificationModalComponent;
  let fixture: ComponentFixture<ActiveAlarmNotificationModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ActiveAlarmNotificationModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ActiveAlarmNotificationModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
