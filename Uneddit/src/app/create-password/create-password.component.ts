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
  protected passStrong = Array(1);
  protected password = "";
  protected repeat = "";
  protected passClassify = "";
  protected repeatEqualToPass = true;
  protected updateStrongBar() {
    let finalStrong = 1;
    if (this.password.length > 3)
      finalStrong++;
    if (this.password.length > 5)
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
      this.barValue=20;
    }
    else if (finalStrong < 5) {
      this.barValue=35;
    }
    else if (finalStrong < 7) {
      this.barValue=50;
    }
    else if (finalStrong < 9) {
      this.barValue=70;
    }
    else {
      this.barValue=100;
    }
  }
  protected updateRepeatCondition() {
    this.repeatEqualToPass = this.password === this.repeat
  }
  protected passwordChanged() {
    this.updateStrongBar()
    this.updateRepeatCondition()
  }
}
