import { Component } from '@angular/core';
import { ROUTES } from './utils/constants/routes';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'inventarioFront';
  ROUTES = ROUTES;
}
