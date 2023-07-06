import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { AddUserComponent } from '../add-user/add-user.component';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ForumData } from '../DTO/Forum/forum-data';
import { GetForumData } from '../DTO/Forum/get-forum-data';
import { ForumService } from '../Services/forum-service/forum.service';
import { ForumUserData } from '../DTO/Forum/forum-user-data';
import { PostData } from '../DTO/Forum/Post/post-data';
import { PostService } from '../Services/post-service/post.service';
import { PostUserComponent } from '../post-user/post-user.component';

@Component({
  selector: 'app-forum-page',
  templateUrl: './forum-page.component.html',
  styleUrls: ['./forum-page.component.css'],
})
export class ForumPageComponent implements OnInit {
  posts: PostData[] = []
  constructor(
    public dialog: MatDialog, private route: ActivatedRoute,
    private router: Router,
    private forumService: ForumService,
    private postService : PostService,
    ) { }
  active= false;
  openDialog() {
    const dialogRef = this.dialog.open(AddUserComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
  data: GetForumData =
    {
      jwt: sessionStorage.getItem('session') ?? "",
      forumName: ''
    }

  forum: ForumData = {
    titulo: '',
    id: 0,
    descricao: '',
    dataCriacao: new Date(),
    jwt: '',
    quantidade: 0
  }

  
  subscription: any;

  goActive()
  {
    this.active = true;
  }
  ngOnInit(): void {
    this.subscription = this.route.params.subscribe(params => {
      this.data.forumName = params["title"]

      this.forumService.getForum(this.data)
        .subscribe({
          next: (res: ForumData) => {
            this.forum = res
          },
          error: (error: any) => {
            this.router.navigate(["**"])
          }
        })

    })

  }

  
}

