import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountPasswordResetComponent } from './account-password-reset/account-password-reset.component';
import { ActiveTimersComponent } from './active-timers/active-timers.component';
import { LayoutComponent } from './layout/layout.component';
import { LoginComponent } from './login/login.component';
import { PastTimersComponent } from './past-timers/past-timers.component';
import { RegisterComponent } from './register/register.component';
import { TimerDetailsComponent } from './timer-details/timer-details.component';
import { UserDetailsComponent } from './user-details/user-details.component';

const routes: Routes = [
  //Without JWT in localStorage [Guard -> redirect to timers/active]
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'resetPassword', component: AccountPasswordResetComponent },
  { path: 'resetPassword/confirmation', component: AccountPasswordResetComponent },

  //Nested inside Layout which contains sidebar, navbar, footer and content-container
  //[Guard -> check JWT exists and its expiration date]
  {
    path: '', component: LayoutComponent, /*redirectTo: '/user/details',*/ children: [
    
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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

