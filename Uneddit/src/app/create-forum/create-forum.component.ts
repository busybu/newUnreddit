import { Component } from '@angular/core';
import { FormControl, FormGroupDirective, FormsModule, NgForm, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ForumData } from '../DTO/Forum/forum-data';
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
  constructor(private forumService: ForumService, 
    private router: Router) {

  }
  responseMessage = "";
  data: ForumData = 
  {
    titulo: '',
    descricao :'',
    dataCriacao: new Date(),
    jwt: sessionStorage.getItem('session') ?? "",
    quantidade: 0
  }
  
  openDialog(): void {
  }
  nameFormControl = new FormControl('', [Validators.required, Validators.email]);
  matcher = new MyErrorStateMatcher();

  createForum()
  {
    console.log(this.data.titulo);
    console.log(this.data.descricao);
    console.log(this.data.quantidade);
    this.forumService.create(this.data)
    .subscribe(res => {
      this.responseMessage = res.message;
      if(this.responseMessage == "Grupo criado")
        this.router.navigate(['/'])
    })
    
  }
}
