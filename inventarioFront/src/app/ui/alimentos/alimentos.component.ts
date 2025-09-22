import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { AlimentoBebidaService } from 'src/app/services/alimento-bebida.service';
import { AlertService } from 'src/app/utils/alerts/alert.service';
import { AlimentoBebidaDTO } from 'src/app/utils/dtos';
import { ModalAlimentosService } from './modal-alimentos/modal-alimentos.service';

@Component({
  selector: 'app-alimentos',
  templateUrl: './alimentos.component.html',
  styleUrls: ['./alimentos.component.css'],
})
export class AlimentosComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  data: AlimentoBebidaDTO[];
  dataSource: MatTableDataSource<AlimentoBebidaDTO>;
  selection: SelectionModel<AlimentoBebidaDTO>;

  constructor(
    private readonly modalAlimentosService: ModalAlimentosService,
    private readonly alimentoBebidaService: AlimentoBebidaService
  ) {
    this.data = [];
    this.dataSource = new MatTableDataSource<AlimentoBebidaDTO>([]);
    this.selection = new SelectionModel<AlimentoBebidaDTO>(true);
  }
  ngOnInit(): void {
    this.paginator._intl.itemsPerPageLabel = 'Registros por página';
    this.getAll();
  }

  private getAll(): void {
    this.alimentoBebidaService.getAll().subscribe((response) => {
      if (response.success) {
        this.data = response.data.map((usuario) =>
          new AlimentoBebidaDTO().deserialize(usuario)
        );
        this.dataSource.data = this.data;
      }
    });
  }

  deleteByConfimation(registro: AlimentoBebidaDTO): void {
    AlertService.confirm(
      'Eliminar registro',
      `¿Deseas eliminar el registro ?`
    ).subscribe((result) => {
      if (!result) {
        return;
      }
      this.alimentoBebidaService.delete(registro.id).subscribe((response) => {
        this.getAll();
        if (response.success) {
          AlertService.success(
            'Registro Eliminado',
            'El registro fue eliminado correctamente'
          );
        }
      });
    });
  }

  add(): void {
    this.modalAlimentosService
      .open()
      .afterClosed()
      .subscribe((res) => {
        this.getAll();
      });
  }
  edit(registro: AlimentoBebidaDTO) {
    this.modalAlimentosService
      .open({ data: registro })
      .afterClosed()
      .subscribe((res) => {
        this.getAll();
      });
  }

  public changeCheck(registro: AlimentoBebidaDTO): void {
    if (registro.estatus) {
      registro.estatus = false;
    } else {
      registro.estatus = true;
    }
    AlertService.confirm(
      'Cambiar estatus de usuario',
      `¿Deseas cambiar el estatus del usuario?`
    ).subscribe((result) => {
      if (!result) {
        this.getAll();
        return;
      }
      this.alimentoBebidaService
        .changeEstatus(registro)
        .subscribe((response) => {
          if (response.success) {
            AlertService.success(
              'Usuario actualizado',
              'El usuario fue actualizado correctamente'
            );
            this.getAll();
          }
        });
    });
  }
}
