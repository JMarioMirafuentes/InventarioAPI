import { Injectable } from '@angular/core';
import { AlimentoBebidaDTO } from 'src/app/utils/dtos';
import { ModalAlimentosComponent } from './modal-alimentos.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
export interface AlimentoBebidaData {
  data: AlimentoBebidaDTO;
}
@Injectable({
  providedIn: 'root',
})
export class ModalAlimentosService {
  constructor(private dialog: MatDialog) {}
  open(data?: AlimentoBebidaData): MatDialogRef<ModalAlimentosComponent> {
    return this.dialog.open<ModalAlimentosComponent, AlimentoBebidaData>(
      ModalAlimentosComponent,
      {
        panelClass: 'modal-alimentos-dialog',
        data: data || null,
        width: '40%',
        height: '40%',
      }
    );
  }
}
