import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, map } from 'rxjs';
import { AlertDialogComponent } from './alert-dialog/alert-dialog.component';
import { AlertType } from '../enums/alert-type';

@Injectable({ providedIn: 'root' })
export class AlertService {
  constructor(private dialog: MatDialog) {}

  private openDialog(
    title: string,
    message: string,
    type: string
  ): Observable<boolean> {
    const dialogRef = this.dialog.open(AlertDialogComponent, {
      width: '400px',
      data: { title, message, type },
    });

    return dialogRef.afterClosed().pipe(map((result) => !!result));
  }

  confirm(title: string, message: string): Observable<boolean> {
    return this.openDialog(title, message, AlertType.CONFIRM);
  }

  error(title: string, message: string): Observable<boolean> {
    return this.openDialog(title, message, AlertType.ERROR);
  }

  warning(title: string, message: string): Observable<boolean> {
    return this.openDialog(title, message, AlertType.WARNING);
  }

  success(title: string, message: string): Observable<boolean> {
    return this.openDialog(title, message, AlertType.SUCCESS);
  }
}
