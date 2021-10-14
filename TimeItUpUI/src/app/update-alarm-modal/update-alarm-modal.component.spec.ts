import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateAlarmModalComponent } from './update-alarm-modal.component';

describe('UpdateAlarmModalComponent', () => {
  let component: UpdateAlarmModalComponent;
  let fixture: ComponentFixture<UpdateAlarmModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateAlarmModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateAlarmModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
