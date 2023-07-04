import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ErrorData } from 'src/app/DTO/error-data';
import { ForumData } from 'src/app/DTO/Forum/forum-data';
import { ForumUserData } from 'src/app/DTO/Forum/forum-user-data';

@Injectable({
  providedIn: 'root'
})
export class ForumService {

  constructor(private http: HttpClient) { }

  create(data : ForumData)
  {
    return this.http.post<ErrorData>("http://localhost:5062/forum/create", data)
  }
  enterForum(data: ForumUserData)
  {
    return this.http.post<ErrorData>("http://localhost:5062/forum/addUser", data)
  }
}
