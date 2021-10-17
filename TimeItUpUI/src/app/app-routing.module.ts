import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AccountPasswordResetComponent } from './account-password-reset';
import { AccountPasswordResetGetTokenComponent } from './account-password-reset-get-token';
import { ActiveTimersComponent } from './active-timers';
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
    path: 'resetPassword',
    canActivate: [NonAuthUserGuard], component: AccountPasswordResetGetTokenComponent
  },
  {
    path: 'resetPassword/confirmation',
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
      },
      {
        path: 'timers/active',
        component: ActiveTimersComponent,
      },
      {
        path: 'timers/past',
        component: PastTimersComponent
      },
      {
        path: 'timer/:id',
        component: TimerDetailsComponent
      },
      {
        path: '',
        component: ActiveTimersComponent,
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

