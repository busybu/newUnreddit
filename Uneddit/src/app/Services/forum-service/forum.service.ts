import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ForumData } from 'src/app/DTO/forum-data';

@Injectable({
  providedIn: 'root'
})
export class ForumService {

  constructor(private http: HttpClient) { }

  create(data : ForumData)
  {
    ///return this.http.post<aaaa>("http://localhost:5062/user/forum", data)
  }
}
