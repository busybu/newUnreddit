import { Component } from '@angular/core';
import { FormControl, FormGroupDirective, FormsModule, NgForm, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ForumData } from '../DTO/forum-data';
import { ForumService } from '../Services/forum-service/forum.service';
import { Router } from '@angular/router';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
@Component({
  selector: 'app-create-forum',
  templateUrl: './create-forum.component.html',
  styleUrls: ['./create-forum.component.css'],

})
export class CreateForumComponent {
  data: ForumData = 
  {
    titulo: '',
    descricao :'',
    dataCriacao: new Date(),
    jwt: localStorage.getItem('session') ?? "",
    quantidade: 0
  }

  constructor(private forumService: ForumService, 
    private router: Router) {

  }
  openDialog(): void {
  }
  nameFormControl = new FormControl('', [Validators.required, Validators.email]);
  matcher = new MyErrorStateMatcher();

  // createForum()
  // {
  //   this.forumService.create(this.data)
  //   .subscribe(res => {
      
  //   })
  // }
}
