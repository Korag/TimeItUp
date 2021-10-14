import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateTimerModalComponent } from './update-timer-modal.component';

describe('UpdateTimerModalComponent', () => {
  let component: UpdateTimerModalComponent;
  let fixture: ComponentFixture<UpdateTimerModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateTimerModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateTimerModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
