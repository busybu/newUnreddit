import { Component } from '@angular/core';
import { FormControl, NgModel, Validators } from '@angular/forms';
import { PostData } from '../DTO/post-data';
import { ForumService } from '../Services/forum-service/forum.service';
import { PostService } from '../Services/post-service/post.service';
import { Router } from '@angular/router';
import { Jwt } from '../DTO/Jwt/jwt-data';
import { ForumData } from '../DTO/Forum/forum-data';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent {
  forums: ForumData[] = []
  value = 0
  responseMessage = ''

  openDialog(): void {
  }
  constructor(private forumService: ForumService,
    private router: Router,
    private postService: PostService) { }

  data: PostData =
    {
      titulo: '',
      conteudo: '',
      dataCriacao: new Date(),
      jwt: sessionStorage.getItem('session') ?? "",
      anexo: '',
      forumID: 0,
      nomeAutor:'',
      nomeForum:''
    }

  jwt: Jwt =
    {
      value: sessionStorage.getItem('session') ?? "",
    }

  getForumSelected() {
    this.forumService.listForumUser(this.jwt)
      .subscribe(res => {
        this.forums = res
      })
  }
  selectForum(idForumSelected: number) {
    this.data.forumID = idForumSelected;
    console.log(this.data.forumID)
  }
  post() {
    this.postService.create(this.data)
      .subscribe(res => {
        this.responseMessage = res.message;
      })
  }
}
