import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { PostService } from '../Services/post-service/post.service';
import { Jwt } from '../DTO/Jwt/jwt-data';
import { Router } from '@angular/router';
import { PostData } from '../DTO/Forum/Post/post-data';
import { ForumUserData } from '../DTO/Forum/forum-user-data';
import { LikeData } from '../DTO/Forum/Post/like-data';
import {
  UserService
} from '../Services/user-service/user.service';
@Component({
  selector: 'app-post-user',
  templateUrl: './post-user.component.html',
  styleUrls: ['./post-user.component.css']
})
export class PostUserComponent implements OnInit, OnChanges {
  @Input() ForumId = 0;
  @Input() isPostActive = false;

  posts: PostData[] = []
  ativo = false
  likeData: LikeData = {
    jwt: '',
    idPost: 0,
    hasLike: false,
    quantity: 0,
    idUser: 0
  }

  dataToGetPosts: ForumUserData = {
    forumId: 0,
    jwt: ''
  }

  constructor(private postService: PostService, private userService: UserService,
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


  getPostFromForum() {
    this.dataToGetPosts = {
      jwt: sessionStorage.getItem('session') ?? "",
      forumId: this.ForumId
    }

    this.postService.getPostFromForum(this.dataToGetPosts)
      .subscribe(res => {
        this.posts = res
      })
  }

  like(id: number) {
    this.likeData =
    {
      jwt: sessionStorage.getItem('session') ?? "",
      idPost: id,
      hasLike: true,
      quantity: 0,
      idUser: 0
    }
    this.postService.likePost(this.likeData)
      .subscribe(res => {
        console.log(res)

        console.log(this.likeData.idPost)
        if (res.idPost == this.likeData.idPost)
          this.likeData = res
        var postAtual = this.posts.filter(x => x.id == id)[0]

        console.log(this.likeData.hasLike)
        if (this.likeData.hasLike != true) {
          postAtual.like -= 1
        }
        else {
          postAtual.like += 1
        }
      })
  }

  verifyUser(id: number) {
    var myId = 0;
    var idPoster = 0;
    var postAtual = this.posts.filter(x => x.id = id)[0]

    idPoster = postAtual.idAutor;
    console.log(idPoster)
    this.userService.getUser(this.jwt)
      .subscribe(res => {
        console.log(res.id)
        myId = res.id
        console.log("idPoster"+idPoster)
        console.log("myId"+myId)

        this.ativo = myId != idPoster
      })

  }

}
