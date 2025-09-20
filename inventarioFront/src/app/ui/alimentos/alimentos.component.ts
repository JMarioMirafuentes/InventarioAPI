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
    private readonly modalFondoService: ModalAlimentosService,
    private readonly fondoService: AlimentoBebidaService
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
    this.fondoService.getAll().subscribe((response) => {
      if (response.success) {
        this.data = response.data.map((usuario) =>
          new AlimentoBebidaDTO().deserialize(usuario)
        );
        this.dataSource.data = this.data;
      }
    });
  }

  deleteByConfimation(codigo: AlimentoBebidaDTO): void {
    AlertService.confirm('Eliminar Código', `¿Deseas eliminar el código?`);
    // .subscribe(
    //   (result) => {
    //     if (!result || !result.isConfirmed) {
    //       return;
    //     }
    //     this.fondoService.deleteFondo(codigo.idFondo).subscribe((response) => {
    //       this.getAll();
    //       if (response.success) {
    //         Alert.success(
    //           'Código Eliminado',
    //           'El código fue eliminado correctamente'
    //         );
    //       }
    //     });
    //   }
  }

  add(): void {
    this.modalFondoService
      .open()
      .afterClosed()
      .subscribe((res) => {
        this.getAll();
      });
  }
  edit(usuario: AlimentoBebidaDTO) {
    this.modalFondoService
      .open({ data: usuario })
      .afterClosed()
      .subscribe((res) => {
        this.getAll();
      });
  }
}
