import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from '../utils/interfaces/response';
import { environment } from '../../environments/environment';
import { AlimentoBebidaDTO } from '../utils/dtos';
@Injectable({
  providedIn: 'root',
})
export class AlimentoBebidaService {
  constructor(private http: HttpClient) {}

  getAll(): Observable<Response<AlimentoBebidaDTO[]>> {
    return this.http.get<Response<AlimentoBebidaDTO[]>>(
      environment.api.concat('/AlimentosBebidas')
    );
  }

  get(idAlimentoBebidaDTO: Number): Observable<Response<AlimentoBebidaDTO>> {
    return this.http.get<Response<AlimentoBebidaDTO>>(
      environment.api.concat(`/AlimentosBebidas/${idAlimentoBebidaDTO}`)
    );
  }
  save(body: AlimentoBebidaDTO): Observable<Response<AlimentoBebidaDTO>> {
    return this.http.post<Response<AlimentoBebidaDTO>>(
      environment.api.concat(`/AlimentosBebidas`),
      body
    );
  }
  update(body: AlimentoBebidaDTO): Observable<Response<AlimentoBebidaDTO>> {
    return this.http.put<Response<AlimentoBebidaDTO>>(
      environment.api.concat(`/AlimentosBebidas`),
      body
    );
  }
  delete(idAlimentoBebidaDTO: Number): Observable<Response<AlimentoBebidaDTO>> {
    return this.http.delete<Response<AlimentoBebidaDTO>>(
      environment.api.concat(`/AlimentosBebidas/${idAlimentoBebidaDTO}`)
    );
  }

  changeEstatus(
    body: AlimentoBebidaDTO
  ): Observable<Response<AlimentoBebidaDTO>> {
    return this.http.patch<Response<AlimentoBebidaDTO>>(
      environment.api.concat(`/AlimentosBebidas/CambiarEstatus`),
      body
    );
  }
}
