import { Component } from '@angular/core';
import { ROUTES } from './utils/constants/routes';
import { LoginService } from './services/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  constructor(readonly loginService: LoginService) {}
  title = 'inventarioFront';
  ROUTES = ROUTES;
  logOut(): void {
    this.loginService.setAutorized(false);
  }
}
