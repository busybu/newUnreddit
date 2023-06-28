import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeedPageComponent } from './feed-page/feed-page.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { ForumPageComponent } from './forum-page/forum-page.component';

const routes: Routes = [
  { path: '', title: "Feed", component: FeedPageComponent },
  { path: 'login', title: "Login", component: LoginPageComponent},
  { path: 'register', title: "Register", component: RegisterPageComponent},
  { path: 'forumPage', title: "ForumPage", component: ForumPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
