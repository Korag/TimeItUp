import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { RequestNonAuthorizedErrorInterceptor } from './_helpers/requestNonAuthorizedError.interceptor';
import { AttachJWTToRequestInterceptor } from './_helpers/attachJWTToRequest.interceptor';

import { ActiveAlarmNotificationModalComponent } from './active-alarm-notification-modal';
import { RemoveAlarmModalComponent } from './remove-alarm-modal';
import { CreateAlarmModalComponent } from './create-alarm-modal';
import { UpdateAlarmModalComponent } from './update-alarm-modal';
import { CreateTimerModalComponent } from './create-timer-modal';
import { RemoveTimerModalComponent } from './remove-timer-modal';
import { UpdateTimerModalComponent } from './update-timer-modal';
import { TimerDetailsComponent } from './timer-details';
import { ActiveTimersComponent } from './active-timers';
import { PastTimersComponent } from './past-timers';
import { UserDetailsComponent } from './user-details';
import { RegisterComponent } from './register';
import { AccountPasswordResetGetTokenComponent } from './account-password-reset-get-token';
import { AccountPasswordResetComponent } from './account-password-reset';
import { FooterComponent } from './footer';
import { LoginComponent } from './login';
import { LeftSidebarComponent } from './left-sidebar';
import { LayoutComponent } from './layout';
import { NavbarComponent } from './navbar';
import { ActiveTimerUnitComponent } from './active-timer-unit';
import { TimerWithControlsComponent } from './timer-with-controls';
import { TimerInfoComponent } from './timer-info';
import { TimerDurationSectionComponent } from './timer-duration-section';
import { TimerAlarmsListComponent } from './timer-alarms-list';
import { TimerSplitsListComponent } from './timer-splits-list';
import { TimerPausesListComponent } from './timer-pauses-list';
import { ErrorComponent } from './error/error.component';

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
    ErrorComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AttachJWTToRequestInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: RequestNonAuthorizedErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent, FooterComponent]
})
export class AppModule { }
