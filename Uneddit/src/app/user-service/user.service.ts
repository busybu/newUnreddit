import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserData } from '../user-data';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) {}

  register(data : UserData)
  {
    return this.http.post("http://localhost:5062/user/register", data)
  }
}

