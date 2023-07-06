import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { PostService } from '../Services/post-service/post.service';
import { Jwt } from '../DTO/Jwt/jwt-data';
import { Router } from '@angular/router';
import { PostData } from '../DTO/Forum/Post/post-data';
import { ForumPageComponent } from '../forum-page/forum-page.component';
import { ForumUserData } from '../DTO/Forum/forum-user-data';
import { LikeData } from '../DTO/Forum/Post/like-data';
@Component({
  selector: 'app-post-user',
  templateUrl: './post-user.component.html',
  styleUrls: ['./post-user.component.css']
})
export class PostUserComponent implements OnInit, OnChanges {
  @Input() ForumId = 0;
  @Input() isPostActive = false;

  posts: PostData[] = []

  constructor(private postService: PostService,
    private router: Router) { }

  ngOnChanges() {
    if (this.ForumId == 0)
      return
    this.getPostFromForum();
  }


  ngOnInit(): void {
    if (this.isPostActive == false)
      this.getAllPost();

    let url = this.router.url
    console.log(url.includes("forum"))
  }

  jwt: Jwt = { value: sessionStorage.getItem('session') ?? "" }

  getAllPost() {
    this.postService.getPosts(this.jwt)
      .subscribe(res => {
        this.posts = res
      })
  }
  dataToGetPosts: ForumUserData = {
    forumId: 0,
    jwt: ''
  }

  getPostFromForum() {
    this.dataToGetPosts = {
      jwt: sessionStorage.getItem('session') ?? "",
      forumId: this.ForumId
    }

    this.postService.getPostFromForum(this.dataToGetPosts)
      .subscribe(res => {
        console.log(res)
        this.posts = res
      })
  }
  likeData: LikeData = {
    jwtUser: '',
    idForum: 0
  }
  like(idForum:number) {
    this.likeData  =
    {
      jwtUser: sessionStorage.getItem('session') ?? "",
      idPost: idForum
    }
    this.postService.likePost(this.)
  }
}
