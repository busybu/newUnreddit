import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ForumService } from '../Services/forum-service/forum.service';
import { Router } from '@angular/router';
import { ForumUserData } from '../DTO/Forum/forum-user-data';
import { ForumData } from 'src/app/DTO/Forum/forum-data';

@Component({
  selector: 'app-list-forums',
  templateUrl: './list-forums.component.html',
  styleUrls: ['./list-forums.component.css']
})
export class ListForumsComponent {
  panelOpenState = false;
  forums: ForumData[] = []
  @Output() forumTitulo = new EventEmitter<string>;
  
  constructor(private forumService: ForumService, private router: Router) { }

  data: ForumUserData =
    {
      forumId: 0,
      jwt: sessionStorage.getItem('session') ?? ""
    }

  list() {
    this.forumService.listForum(this.data)
      .subscribe(res => {
        this.forums = res
        console.log("uai: " + this.forums)
      })
  }

  teste(forumTitulo: string){
    if(forumTitulo){
      //oia ela usa serviço q chiqueh
      this.router.navigate(["forum/" + forumTitulo]);
    }
  }
}
