import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AccountPasswordResetComponent } from './account-password-reset';
import { AccountPasswordResetGetTokenComponent } from './account-password-reset-get-token';
import { ActiveTimersComponent } from './active-timers';
import { CreateTimerComponent } from './create-timer';
import { ErrorComponent } from './error';
import { LayoutComponent } from './layout';
import { LoginComponent } from './login';
import { PastTimersComponent } from './past-timers';
import { RegisterComponent } from './register';
import { TimerDetailsComponent } from './timer-details';
import { UserDetailsComponent } from './user-details';

import { AuthUserGuard, NonAuthUserGuard } from './_helpers';

const routes: Routes = [
  //Without JWT in localStorage [Guard -> redirect to timers/active]
  {
    path: 'register',
    canActivate: [NonAuthUserGuard], component: RegisterComponent
  },
  {
    path: 'login',
    canActivate: [NonAuthUserGuard], component: LoginComponent
  },
  {
    path: 'getResetPasswordToken',
    canActivate: [NonAuthUserGuard], component: AccountPasswordResetGetTokenComponent
  },
  {
    path: 'resetPassword/email/:email/token/:token',
    canActivate: [NonAuthUserGuard], component: AccountPasswordResetComponent
  },

  {
    path: 'error',
    component: ErrorComponent
  },

  //Nested inside Layout which contains sidebar, navbar, footer and content-container
  //[Guard -> check JWT exists and its expiration date]
  {
    path: '', component: LayoutComponent, canActivate: [AuthUserGuard], children: [
      {
        path: 'user/details',
        component: UserDetailsComponent,
        canActivate: [AuthUserGuard]
      },
      {
        path: 'timers/active',
        component: ActiveTimersComponent,
        canActivate: [AuthUserGuard]
      },
      {
        path: 'timers/past',
        component: PastTimersComponent,
        canActivate: [AuthUserGuard]
      },
      {
        path: 'timers/add',
        component: CreateTimerComponent,
        canActivate: [AuthUserGuard]
      },
      {
        path: 'timer/details/:id',
        component: TimerDetailsComponent,
        canActivate: [AuthUserGuard]
      },
      {
        path: '',
        component: ActiveTimersComponent,
        canActivate: [AuthUserGuard]
      },
    ],
  },
  { path: '**', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

