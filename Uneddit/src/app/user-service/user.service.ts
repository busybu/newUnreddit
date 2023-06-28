import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserData } from '../user-data';
import { LoginData } from '../login-data';
import { ErrorData } from '../error-data';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) {}

  register(data : UserData)
  {
    return this.http.post<ErrorData>("http://localhost:5062/user/register", data)
  }
  login(data: LoginData)
  {
    return this.http.post("", data)
  }
}

