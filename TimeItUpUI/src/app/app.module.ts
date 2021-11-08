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
import { RemoveTimerModalComponent } from './remove-timer-modal';
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
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LogoutUserModalComponent } from './logout-user-modal/logout-user-modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CreateTimerComponent } from './create-timer';

import { DataTablesModule } from "angular-datatables";

import { OwlDateTimeModule, OwlNativeDateTimeModule, OWL_DATE_TIME_LOCALE } from 'ng-pick-datetime';

@NgModule({
  declarations: [
    AppComponent,
    CreateAlarmModalComponent,
    ActiveAlarmNotificationModalComponent,
    RemoveAlarmModalComponent,
    UpdateAlarmModalComponent,
    CreateTimerComponent,
    RemoveTimerModalComponent,
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
    LogoutUserModalComponent,
    CreateTimerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    NgbModule,
    DataTablesModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AttachJWTToRequestInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: RequestNonAuthorizedErrorInterceptor, multi: true },
    { provide: OWL_DATE_TIME_LOCALE, useValue: 'in' },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
