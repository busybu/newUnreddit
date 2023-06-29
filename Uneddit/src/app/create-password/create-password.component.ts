import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-create-password',
  templateUrl: './create-password.component.html',
  styleUrls: ['./create-password.component.css']
})
export class CreatePasswordComponent {
  hide = true;
  barValue = 0;
  protected inputText = "";
  public strongfront = 0;
  protected passStrong = Array(1);
  protected password = "";
  protected repeat = "";
  protected passClassify = "";
  protected repeatEqualToPass = true;

  @Output() seePasswordChanged = new EventEmitter<string>();
  @Output() Strong = new EventEmitter<number>();
  
  public updateStrongBar() {
    let finalStrong = 1;
    if (this.password.length > 5)
      finalStrong++;
    if (this.password.length > 6)
      finalStrong++;
    if (this.password.length > 7)
      finalStrong++;
    if (this.password.length > 9)
      finalStrong++;
    if (this.password.match("[a-z]") != null)
      finalStrong++;
    if (this.password.match("[A-Z]") != null)
      finalStrong++;
    if (this.password.match("[0-9]") != null)
      finalStrong++;
    if (this.password.match("[\W]") != null)
      finalStrong++;
    this.passStrong = Array(finalStrong);
    if (finalStrong < 3) {
      this.barValue=0;
    }
    else if (finalStrong < 3) {
      this.barValue=35;
    }
    else if (finalStrong < 5) {
      this.barValue=50;
    }
    else if (finalStrong < 7) {
      this.barValue=70;
    }
    else if (finalStrong < 9) {
      this.barValue=100;
    }
    else {
      this.barValue=100;
    }
    this.strongfront = finalStrong;
    this.Strong.emit(finalStrong);
    this.seePasswordChanged.emit(this.password);
  }
  protected updateRepeatCondition() {
    this.repeatEqualToPass = this.password === this.repeat
  }
  protected passwordChanged() {
    this.updateStrongBar()
    this.updateRepeatCondition()
  }
}
