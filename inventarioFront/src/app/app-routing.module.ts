import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AlimentosComponent } from './ui/alimentos/alimentos.component';
import { ROUTES } from './utils/constants/routes';
import { LoginComponent } from './ui/login/login.component';

const routes: Routes = [
  { path: ROUTES.TABLE, component: AlimentosComponent },
  { path: ROUTES.LOGIN, component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
