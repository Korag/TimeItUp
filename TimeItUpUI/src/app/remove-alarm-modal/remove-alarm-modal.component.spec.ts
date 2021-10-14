import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoveAlarmModalComponent } from './remove-alarm-modal.component';

describe('RemoveAlarmModalComponent', () => {
  let component: RemoveAlarmModalComponent;
  let fixture: ComponentFixture<RemoveAlarmModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemoveAlarmModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoveAlarmModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
