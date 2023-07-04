import { Component, Input } from '@angular/core';
import { ForumUserData } from '../DTO/Forum/forum-user-data';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent {
  errorMessage='';
  
  @Input() GroupId : number = 0

  
  enterForum(){
    
  }
}
