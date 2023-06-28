import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserData } from '../DataTransferObj/user-data';
import { LoginData } from '../DataTransferObj/login-data';
import { ErrorData } from '../DataTransferObj/error-data';
import { LoginDTO } from '../DataTransferObj/login-return';

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
    return this.http.post<LoginDTO>("http://localhost:5062/user/login", data)
  }
}

