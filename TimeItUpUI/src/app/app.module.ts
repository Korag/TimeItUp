import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { CreateAlarmModalComponent } from './create-alarm-modal/create-alarm-modal.component';
import { ActiveAlarmNotificationModalComponent } from './active-alarm-notification-modal/active-alarm-notification-modal.component';
import { RemoveAlarmModalComponent } from './remove-alarm-modal/remove-alarm-modal.component';
import { UpdateAlarmModalComponent } from './update-alarm-modal/update-alarm-modal.component';
import { CreateTimerModalComponent } from './create-timer-modal/create-timer-modal.component';
import { RemoveTimerModalComponent } from './remove-timer-modal/remove-timer-modal.component';
import { UpdateTimerModalComponent } from './update-timer-modal/update-timer-modal.component';
import { TimerDetailsComponent } from './timer-details/timer-details.component';
import { ActiveTimersComponent } from './active-timers/active-timers.component';
import { PastTimersComponent } from './past-timers/past-timers.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { AccountPasswordResetGetTokenComponent } from './account-password-reset-get-token/account-password-reset-get-token.component';
import { AccountPasswordResetComponent } from './account-password-reset/account-password-reset.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { FooterComponent } from './footer/footer.component';
import { LeftSidebarComponent } from './left-sidebar/left-sidebar.component';
import { LayoutComponent } from './layout/layout.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ActiveTimerUnitComponent } from './active-timer-unit/active-timer-unit.component';
import { TimerWithControlsComponent } from './timer-with-controls/timer-with-controls.component';
import { TimerInfoComponent } from './timer-info/timer-info.component';
import { TimerDurationSectionComponent } from './timer-duration-section/timer-duration-section.component';
import { TimerAlarmsListComponent } from './timer-alarms-list/timer-alarms-list.component';
import { TimerSplitsListComponent } from './timer-splits-list/timer-splits-list.component';
import { TimerPausesListComponent } from './timer-pauses-list/timer-pauses-list.component';

import { RequestNonAuthorizedErrorInterceptor } from './_helpers/requestNonAuthorizedError.interceptor';
import { AttachJWTToRequestInterceptor } from './_helpers/attachJWTToRequest.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    CreateAlarmModalComponent,
    ActiveAlarmNotificationModalComponent,
    RemoveAlarmModalComponent,
    UpdateAlarmModalComponent,
    CreateTimerModalComponent,
    RemoveTimerModalComponent,
    UpdateTimerModalComponent,
    TimerDetailsComponent,
    ActiveTimersComponent,
    PastTimersComponent,
    UserDetailsComponent,
    AccountPasswordResetGetTokenComponent,
    AccountPasswordResetComponent,
    RegisterComponent,
    LoginComponent,
    FooterComponent,
    LeftSidebarComponent,
    LayoutComponent,
    NavbarComponent,
    ActiveTimerUnitComponent,
    TimerWithControlsComponent,
    TimerInfoComponent,
    TimerDurationSectionComponent,
    TimerAlarmsListComponent,
    TimerSplitsListComponent,
    TimerPausesListComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AttachJWTToRequestInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: RequestNonAuthorizedErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
