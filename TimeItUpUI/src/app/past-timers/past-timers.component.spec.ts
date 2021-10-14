import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PastTimersComponent } from './past-timers.component';

describe('PastTimersComponent', () => {
  let component: PastTimersComponent;
  let fixture: ComponentFixture<PastTimersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PastTimersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PastTimersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
