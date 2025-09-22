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
import { catchError, map, tap } from 'rxjs/operators';
import { AlertService } from '../utils/alerts/alert.service';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
  constructor(private readonly alertService: AlertService) {}
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      tap((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          const body: any = event.body;
          if (!body.success && !request.reportProgress) {
            throw new HttpErrorResponse({
              error: { message: body.message || 'Error inesperado' },
              headers: event.headers,
              status: body.statusCode || null,
              statusText: body.message || 'Error inesperado',
              url: event.url!,
            });
          }
        }
      }),
      catchError((error: HttpErrorResponse) => {
        let message = 'Error inesperado';
        if ([400, 404, 409].includes(error.status)) {
          message = error.error?.message || message;
          this.alertService.error('Error', message).subscribe();
        } else if ([503, 504].includes(error.status)) {
          this.alertService
            .warning(
              'Error',
              'No fue posible realizar tu petición. Favor de intentar nuevamente'
            )
            .subscribe();
        } else if (error.status === 0) {
          this.alertService
            .warning('Error', 'No hay conexión con el servidor')
            .subscribe();
        } else {
          this.alertService
            .warning('Error', error.error?.message || message)
            .subscribe();
        }

        return throwError(() => error);
      })
    );
  }
}
