import { Component, OnInit } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import { UserInfo } from '../DTO/user-info';
import { UserService } from '../user-service/user.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-feed-page',
  templateUrl: './feed-page.component.html',
  styleUrls: ['./feed-page.component.css']
})
export class FeedPageComponent implements OnInit {

  constructor(private userService: UserService, private router: Router) { }

  userInfo : UserInfo = {
    username: '',
    email: '',
    profilePic: 0
  }

  ngOnInit(): void {
    let jwtAtual = sessionStorage.getItem("session") ?? ""

    if(jwtAtual == "") {
      console.log("ue")
      this.router.navigate(["/home"])
    }


    this.userService.getUser({ value: jwtAtual })
      .subscribe({
        next: (res : UserInfo) => {
          this.userInfo = res

          console.log(this.userInfo)
        },
        error: (error : any) => {
          console.log(error)
        }
        
      })

  }
}
