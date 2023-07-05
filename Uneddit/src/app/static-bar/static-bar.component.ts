import { Component } from '@angular/core';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';
import { CreateForumComponent } from '../create-forum/create-forum.component';
import { CreatePostComponent } from '../create-post/create-post.component';

@Component({
  selector: 'app-static-bar',
  templateUrl: './static-bar.component.html',
  styleUrls: ['./static-bar.component.css']
})
export class StaticBarComponent {
  constructor(public dialog: MatDialog) {}

  openDialog() {
    const dialogRef = this.dialog.open(CreateForumComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  openCreatePost() {
    const dialogRef = this.dialog.open(CreatePostComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
}
