import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginDTO } from '../utils/dtos';
import { Response } from '../utils/interfaces/response';
@Injectable({
  providedIn: 'root',
})
export class LoginService {
  autorizado: boolean = false;
  constructor(private http: HttpClient) {}

  login(login: LoginDTO): Observable<Response<boolean>> {
    return this.http.post<Response<boolean>>(
      environment.api.concat(`/Login/login`),
      login
    );
  }
  setAutorized(value: boolean) {
    this.autorizado = value;
  }
  getAutorized(): boolean {
    return this.autorizado;
  }
}
