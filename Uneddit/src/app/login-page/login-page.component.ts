import { Component, Inject, LOCALE_ID } from '@angular/core';
import { formatDate } from '@angular/common';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  constructor(@Inject(LOCALE_ID) private locale: string) {
    this.today = formatDate(Date.now(), 'yyyy-MM-dd', this.locale);
  }
  emailFormControl = new FormControl('', [Validators.required, Validators.email]);
  hide = true;
  today;
}
