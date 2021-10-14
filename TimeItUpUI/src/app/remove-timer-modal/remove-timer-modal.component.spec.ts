import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoveTimerModalComponent } from './remove-timer-modal.component';

describe('RemoveTimerModalComponent', () => {
  let component: RemoveTimerModalComponent;
  let fixture: ComponentFixture<RemoveTimerModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemoveTimerModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoveTimerModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
