import { Component, Inject, LOCALE_ID } from '@angular/core';
import { formatDate } from '@angular/common';
import { FormControl, Validators } from '@angular/forms';
import { UserData } from '../user-data';
import { UserService } from '../user-service/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent
{
  data: UserData =
  {
    email: '',
    dataNascimento: new Date(),
    password: '',
    username: ''
  }
  constructor(@Inject(LOCALE_ID) private locale: string, 
    private userService: UserService,
    private router: Router) {
    this.today = formatDate(Date.now(), 'yyyy-MM-dd', this.locale);
  }
  
  emailFormControl = new FormControl('', [Validators.required, Validators.email]);
  hide = true;
  today;
  register() {

    this.userService.register(this.data)
      .subscribe(res => {
        this.router.navigate(["/"])
      })
    // this.service.register(
    //   {
    //     username: this.username,
    //     email: this.email,
    //     password: this.password,
    //     dataNascimento: this.dataNascimento
    //   }
    // )
    
  }
}