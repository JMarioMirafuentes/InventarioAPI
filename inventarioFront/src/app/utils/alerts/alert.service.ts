import { MatDialog } from '@angular/material/dialog';
import { AlertDialogComponent } from './alert-dialog/alert-dialog.component';
import { Observable, map } from 'rxjs';
export class AlertService {
  private static dialog: MatDialog;

  static init(dialog: MatDialog) {
    this.dialog = dialog;
  }

  private static openDialog(
    title: string,
    message: string,
    type: string
  ): Observable<boolean> {
    const dialogRef = this.dialog.open(AlertDialogComponent, {
      width: '400px',
      data: { title, message, type },
    });

    return dialogRef.afterClosed().pipe(map((result) => !!result)); // true o false
  }

  static confirm(title: string, message: string): Observable<boolean> {
    return this.openDialog(title, message, 'confirm');
  }

  static error(title: string, message: string): Observable<boolean> {
    return this.openDialog(title, message, 'error');
  }

  static warning(title: string, message: string): Observable<boolean> {
    return this.openDialog(title, message, 'warning');
  }

  static success(title: string, message: string): Observable<boolean> {
    return this.openDialog(title, message, 'success');
  }
}
