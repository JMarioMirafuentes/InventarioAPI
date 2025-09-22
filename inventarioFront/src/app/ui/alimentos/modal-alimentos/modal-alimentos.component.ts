import { Component, Inject, OnInit } from '@angular/core';
import { AlimentoBebidaDTO } from 'src/app/utils/dtos';
import { AlimentoBebidaData } from './modal-alimentos.service';
import { AlimentoBebidaService } from 'src/app/services/alimento-bebida.service';
import { AlertService } from 'src/app/utils/alerts/alert.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
export enum ModalTitle {
  NEW = 'Agregar alimento o bebida.',
  EDIT = 'Editar alimento o bebida.',
}
@Component({
  selector: 'app-modal-alimentos',
  templateUrl: './modal-alimentos.component.html',
  styleUrls: ['./modal-alimentos.component.css'],
})
export class ModalAlimentosComponent implements OnInit {
  recordForm: FormGroup;
  title: ModalTitle;
  data: AlimentoBebidaDTO;
  edit: boolean;
  subscription: Subscription;
  disabled: boolean;

  constructor(
    @Inject(MAT_DIALOG_DATA)
    public readonly alimentoBebidaData: AlimentoBebidaData,
    private readonly formBuilder: FormBuilder,
    private readonly alimentoBebidaService: AlimentoBebidaService,
    private readonly alertService: AlertService,
    private readonly ref: MatDialogRef<never>
  ) {
    this.title = ModalTitle.NEW;
    this.data = new AlimentoBebidaDTO();
    this.edit = false;
    this.disabled = false;
    this.subscription = new Subscription();
    this.recordForm = this.formBuilder.group({
      nombre: [null, [Validators.required]],
      descripcion: [null, [Validators.required]],
      estatus: [true],
    });
  }
  ngOnInit(): void {
    this.title = this.alimentoBebidaData ? ModalTitle.EDIT : ModalTitle.NEW;
    if (this.alimentoBebidaData) {
      this.alimentoBebidaService
        .get(this.alimentoBebidaData.data.id)
        .subscribe((response) => {
          if (!response.success) {
            return;
          }
          const data = new AlimentoBebidaDTO().deserialize(response.data);
          this.data = data;
          this.recordForm.patchValue(data);

          this.edit = true;
          this.trackingStatusForm();
        });
    } else {
      this.trackingStatusForm();
    }
  }

  submit(): void {
    this.recordForm.markAllAsTouched();
    if (this.recordForm.invalid) {
      this.alertService.error('', 'Verifique que los campos sean correctos');
      return;
    }
    const tmp = this.recordForm.getRawValue();
    const registro: AlimentoBebidaDTO = new AlimentoBebidaDTO().deserialize(
      tmp
    );

    if (this.data.id > 0) {
      registro.id = this.data.id;
      this.alimentoBebidaService.update(registro).subscribe((response) => {
        if (response.success) {
          this.alertService.confirm(
            'Información registrada',
            'El registro se ha modificado correctamente'
          );
          this.ref.close(true);
        }
      });
    } else {
      this.alimentoBebidaService.save(registro).subscribe((response) => {
        if (response.success) {
          this.alertService.confirm(
            'Información registrada',
            'El registro se ha creado correctamente'
          );
          this.ref.close(true);
        }
      });
    }
  }
  close(): void {
    this.closeModalByConfimation();
  }
  private closeModalByConfimation(): void {
    if (!this.edit) {
      this.ref.close();
      return;
    }
    this.alertService
      .confirm(
        'Alerta',
        '¿Está seguro de que desea salir? Los datos ingresados no serán guardados.'
      )
      .subscribe((result) => {
        if (!result) {
          return;
        }
        this.ref.close();
      });
  }

  private trackingStatusForm(): void {
    this.subscription.add(
      this.recordForm.statusChanges.subscribe(() => (this.edit = true))
    );
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
