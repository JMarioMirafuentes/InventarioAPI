import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/login.service';
import { AlertService } from 'src/app/utils/alerts/alert.service';
import { ROUTES } from 'src/app/utils/constants/routes';
import { LoginDTO } from 'src/app/utils/dtos';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  userRecordForm: FormGroup;
  noAutorizado: boolean = false;
  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly loginService: LoginService,
    private readonly alertService: AlertService,
    private router: Router
  ) {
    this.userRecordForm = this.formBuilder.group({
      login: [null, [Validators.required]],
      contrasenia: [null, [Validators.required]],
    });
  }
  ngOnInit(): void {}

  submit() {
    this.userRecordForm.markAllAsTouched();
    if (this.userRecordForm.invalid) {
      this.alertService.error('', 'Verifique que los campos sean correctos');
      return;
    }

    const tmp = this.userRecordForm.getRawValue();
    const login: LoginDTO = new LoginDTO().deserialize(tmp);
    this.loginService.login(login).subscribe((res) => {
      const usuarioExiste: boolean = res.data;
      console.log(usuarioExiste);
      if (usuarioExiste) {
        this.loginService.setAutorized(usuarioExiste);
        this.router.navigate([ROUTES.TABLE]);
      }
    });
  }

  removeSpaces(controlName: string): void {
    const control = this.userRecordForm.get(controlName);
    if (control) {
      const noSpaces = control.value?.replace(/\s+/g, '');
      control.setValue(noSpaces, { emitEvent: false });
    }
  }
}
