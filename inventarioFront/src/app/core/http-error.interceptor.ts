import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { AlertService } from '../utils/alerts/alert.service';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
  constructor(
    private readonly dialogRef: MatDialog,
    private readonly router: Router
  ) {}
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      map((ev: HttpEvent<any>) => {
        if (ev instanceof HttpResponse) {
          const body: any = ev.body;

          if (request.reportProgress) {
            return ev;
          }
          if (!body.success) {
            throw new HttpErrorResponse({
              error: { message: body.message || 'Error inesperado' },
              headers: ev.headers,
              status: body.statusCode ? body.statusCode : null,
              statusText: body.message || 'Error inesperado',
              url: ev.url!,
            });
          }
        }
        return ev;
      }),
      catchError((error: HttpErrorResponse) => {
        if (
          error.status === 400 ||
          error.status === 409 ||
          error.status === 404
        ) {
          AlertService.error(
            'Error',
            error.error ? error.error.message : 'Error inesperado'
          );
        } else if (error.status === 503 || error.status === 504) {
          AlertService.warning(
            'Error',
            'No fue posible realiza tu petición. Favor de intentar nuevamente'
          );
        } else {
          console.error('Error from error interceptor', error);
          AlertService.warning(
            'Error',
            error.error ? error.error.message : 'Error inesperado'
          );
        }
        return throwError(() => {});
      })
    );
  }
}
