import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountPasswordResetGetTokenComponent } from './account-password-reset-get-token.component';

describe('AccountPasswordResetGetTokenComponent', () => {
  let component: AccountPasswordResetGetTokenComponent;
  let fixture: ComponentFixture<AccountPasswordResetGetTokenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountPasswordResetGetTokenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountPasswordResetGetTokenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
