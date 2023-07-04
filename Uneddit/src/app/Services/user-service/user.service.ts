import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserData } from '../../DTO/User/user-data';
import { LoginData } from '../../DTO/User/login-data';
import { ErrorData } from '../../DTO/error-data';
import { LoginDTO } from '../../DTO/Jwt/login-return';
import { Jwt } from '../../DTO/Jwt/jwt-data';
import { UserInfo } from '../../DTO/User/user-info';

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
  validateUser(data : Jwt)
  {
    return this.http.post<Jwt>("http://localhost:5062/user/validate", data)
  }

  getUser(data: Jwt)
  {
    return this.http.post<UserInfo>("http://localhost:5062/user/get", data)
  }
}

