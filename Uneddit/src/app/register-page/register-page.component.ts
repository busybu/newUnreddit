import { Component, Inject, LOCALE_ID } from '@angular/core';
import { formatDate } from '@angular/common';
import { FormControl, Validators } from '@angular/forms';
import { UserData } from '../DataTransferObj/user-data';
import { UserService } from '../user-service/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent {
  data: UserData =
    {
      email: '',
      dataNascimento: new Date(),
      password: '',
      username: ''
    }
  passwordStrong = 0;

  constructor(@Inject(LOCALE_ID) private locale: string,
    private userService: UserService,
    private router: Router) {
    this.today = formatDate(Date.now(), 'yyyy-MM-dd', this.locale);
  }
  emailFormControl = new FormControl('', [Validators.required, Validators.email]);
  hide = true;
  today;
  responseMessage = "";

  onPasswordStrongChanged(newValue: number) {
    this.passwordStrong = newValue;
  }

  register() {
    if (this.data.email == '' && this.data.username == '')
      return;

    if (this.passwordStrong < 7)
      return;

    if (!this.emailFormControl.valid)
      return;

    this.userService.register(this.data)
      .subscribe(res => {
        this.responseMessage = res.message;
        if (this.responseMessage == "Usuário Registrado")
          this.router.navigate(["/"])
      })

    this.responseMessage = "Existem campos que não foram preenchidos."
  }
}