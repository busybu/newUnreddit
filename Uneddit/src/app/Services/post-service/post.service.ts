import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Jwt } from 'src/app/DTO/Jwt/jwt-data';
import { ErrorData } from 'src/app/DTO/error-data';
import { PostData } from 'src/app/DTO/post-data';

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
}
