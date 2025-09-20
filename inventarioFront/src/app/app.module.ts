import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AlertDialogComponent } from './utils/alerts/alert-dialog/alert-dialog.component';
import { MaterialModule } from './utils/material/material';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { HttpErrorInterceptor } from './core/http-error.interceptor';
import { MatDialog } from '@angular/material/dialog';
import { AlertService } from './utils/alerts/alert.service';
import { AlimentosComponent } from './ui/alimentos/alimentos.component';
import { LoginComponent } from './ui/login/login.component';
import { ModalAlimentosComponent } from './ui/alimentos/modal-alimentos/modal-alimentos.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    AlertDialogComponent,
    AlimentosComponent,
    LoginComponent,
    ModalAlimentosComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MaterialModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
  constructor(dialog: MatDialog) {
    AlertService.init(dialog);
  }
}
