import { Component } from '@angular/core';
import { FormControl, NgModel, Validators } from '@angular/forms';
import { PostData } from '../DTO/post-data';
import { ForumService } from '../Services/forum-service/forum.service';
import { Router } from '@angular/router';
import { Jwt } from '../DTO/Jwt/jwt-data';
import { ForumData } from '../DTO/Forum/forum-data';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent {
  forums: ForumData[] = []
  
  constructor(private forumService: ForumService,private router: Router) {}

  data: PostData = 
  {
    titulo: '',
    conteudo :'',
    dataCriacao: new Date(),
    jwt: sessionStorage.getItem('session') ?? "",
    anexo: '',
    forumID: 0
  }

  jwt: Jwt = 
  {
    value: sessionStorage.getItem('session') ?? "",
  }
  
  getForumSelected()
  {
      this.forumService.listForumUser(this.jwt)
        .subscribe(res => {
          this.forums = res
        })
      console.log(this.data.forumID)
  }
   
  post()
  {

  }
}
