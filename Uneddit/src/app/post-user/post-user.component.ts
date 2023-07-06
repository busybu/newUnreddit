import { Component, OnInit } from '@angular/core';
import { PostService } from '../Services/post-service/post.service';
import { Jwt } from '../DTO/Jwt/jwt-data';
import { Router } from '@angular/router';
import { PostData } from '../DTO/post-data';

@Component({
  selector: 'app-post-user',
  templateUrl: './post-user.component.html',
  styleUrls: ['./post-user.component.css']
})
export class PostUserComponent implements OnInit{
  ulrImagem = "https://th.bing.com/th/id/OIP.btt2a_-XUUfhmjZm55LkWAHaE7?pid=ImgDet&rs=1"
  posts: PostData[] = []
  
  constructor(private postSerivce: PostService, private router: Router) {}
  ngOnInit(): void {
    this.getAllPost();
  }
  jwt: Jwt = {value: sessionStorage.getItem('session') ?? ""}
  getAllPost()
  {
    this.postSerivce.getPosts(this.jwt)
    .subscribe(res =>{
      this.posts = res
    })
  }
}
