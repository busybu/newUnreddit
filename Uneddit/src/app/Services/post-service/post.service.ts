import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Jwt } from 'src/app/DTO/Jwt/jwt-data';
import { ErrorData } from 'src/app/DTO/error-data';
import { PostData } from 'src/app/DTO/Forum/Post/post-data';
import { ForumUserData } from 'src/app/DTO/Forum/forum-user-data';
import { LikeData } from 'src/app/DTO/Forum/Post/like-data';
import { PostId } from 'src/app/DTO/Forum/Post/post-id';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient) {}

  create(data: PostData)
  {
    return this.http.post<ErrorData>("http://localhost:5062/post/create", data)
  }
  getPosts(jwt: Jwt)
  {
    return this.http.post<PostData[]>("http://localhost:5062/post/listPost", jwt)
  }
  getPostFromForum(data: ForumUserData)
  {
    return this.http.post<PostData[]>("http://localhost:5062/post/getPostFromForum", data)
  }
  likePost(data: LikeData)
  {
    return this.http.post<LikeData>("http://localhost:5062/post/likePost", data)
  }
  
  deletePost(data: PostId)
  {
    return this.http.post<ErrorData>("http://localhost:5062/post/deletePost", data)
  }
}
