import { Component, Inject } from '@angular/core';
import { PostService } from '../Services/post-service/post.service';
import { PostId } from '../DTO/Forum/Post/post-id';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-post',
  templateUrl: './delete-post.component.html',
  styleUrls: ['./delete-post.component.css']
})
export class DeletePostComponent {

  data: PostId = {
    id: 0,
    jwt: sessionStorage.getItem("session") ?? ""
  }
  constructor(@Inject(MAT_DIALOG_DATA) public idPost: any,
   private postService: PostService, private router: Router,
    private _snackBar: MatSnackBar,) { }
  excluir() {
    this.data = {
      id: this.idPost,
      jwt: sessionStorage.getItem('session') ?? ""
    }
    this.postService.deletePost(this.data)
      .subscribe(res => {
        var result = res.message
        this.openSnackBar(result)
      })
  }
  openSnackBar(result: string) {
    this._snackBar.open(result, "sair");
  }
}
