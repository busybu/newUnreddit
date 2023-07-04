import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { PostData } from '../DTO/Forum/Post/post-data';
interface Animal {
  name: string;
  sound: string;
}
@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent {

  data: PostData = 
  {
    titulo: '',
    conteudo :'',
    dataCriacao: new Date(),
    jwt: sessionStorage.getItem('session') ?? "",
    anexo: '',
    forumID: 0,
  }
  
  listForums = Array(1);

  getForumSelected()
  {
    
  }

}
