import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthService } from '../_services';

@Injectable()
export class AttachJWTToRequestInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let loggedUser = this.authService.loggedUserData;
    if (loggedUser && loggedUser.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${loggedUser.token}`
        }
      });
    }

    return next.handle(request);
  }
}
