import { Component, Inject, LOCALE_ID } from '@angular/core';
import { formatDate } from '@angular/common';
import { FormControl, Validators } from '@angular/forms';
import { LoginData } from '../DTO/login-data';
import { UserService } from '../user-service/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  data: LoginData= 
  {
    email: "",
    password: ""
  }
  emailFormControl = new FormControl('', [Validators.required]);
  hide = true;
  userState ='';
  constructor(private userService: UserService, private router: Router)
  {

  }
  verifyUser()
  {
    this.userService.login(this.data)
      .subscribe(res =>
        {
          console.log(res)
          if(!res.sucess)
            this.userState = "Usuário/Senha incorreto(a)"

          if(res.jwt != null)
          {
            sessionStorage.setItem("session",  res.jwt)
            this.router.navigate(["/"])
          }
          
        })

  }
}
