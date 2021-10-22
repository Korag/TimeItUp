import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogoutUserModalComponent } from './logout-user-modal.component';

describe('LogoutUserModalComponent', () => {
  let component: LogoutUserModalComponent;
  let fixture: ComponentFixture<LogoutUserModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LogoutUserModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LogoutUserModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
