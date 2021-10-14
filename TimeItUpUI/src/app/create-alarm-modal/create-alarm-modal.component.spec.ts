import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateAlarmModalComponent } from './create-alarm-modal.component';

describe('CreateAlarmModalComponent', () => {
  let component: CreateAlarmModalComponent;
  let fixture: ComponentFixture<CreateAlarmModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateAlarmModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateAlarmModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
