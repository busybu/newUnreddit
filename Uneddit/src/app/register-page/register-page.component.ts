import { Component, Inject, LOCALE_ID } from '@angular/core';
import { formatDate } from '@angular/common';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent {
  constructor(@Inject(LOCALE_ID) private locale: string) {
    this.today = formatDate(Date.now(), 'yyyy-MM-dd', this.locale);
  }
  emailFormControl = new FormControl('', [Validators.required, Validators.email]);
  hide = true;
  today;
}