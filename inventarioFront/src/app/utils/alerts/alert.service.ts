import { MatDialog } from '@angular/material/dialog';
import { AlertDialogComponent } from './alert-dialog/alert-dialog.component';
export class AlertService {
  private static dialog: MatDialog;

  static init(dialog: MatDialog) {
    this.dialog = dialog;
  }

  private static openDialog(title: string, message: string, type: string) {
    this.dialog.open(AlertDialogComponent, {
      width: '400px',
      data: { title, message, type },
    });
  }

  static confirm(title: string, message: string) {
    this.openDialog(title, message, 'confirm');
  }

  static error(title: string, message: string) {
    this.openDialog(title, message, 'error');
  }

  static warning(title: string, message: string) {
    this.openDialog(title, message, 'warning');
  }
}
