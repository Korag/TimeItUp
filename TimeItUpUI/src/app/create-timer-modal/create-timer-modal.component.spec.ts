import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTimerModalComponent } from './create-timer-modal.component';

describe('CreateTimerModalComponent', () => {
  let component: CreateTimerModalComponent;
  let fixture: ComponentFixture<CreateTimerModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateTimerModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateTimerModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
