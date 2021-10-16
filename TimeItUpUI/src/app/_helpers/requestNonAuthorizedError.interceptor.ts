import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services';
import { Router } from '@angular/router';

@Injectable()
export class RequestNonAuthorizedErrorInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService,
              private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {

      if (err.status === 401) {
        this.authService.logout();
        location.reload(true);
      }

      this.router.navigate(["/error"]);
      const error = err.error.message || err.statusText;
      return throwError(error);
    }))
  }
}
