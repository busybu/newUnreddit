import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeedPageComponent } from './feed-page/feed-page.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { ForumPageComponent } from './forum-page/forum-page.component';
import { HomePageComponent } from './home-page/home-page.component';

const routes: Routes = [
  { path: '', title: "Feed", component: FeedPageComponent },
  { path: 'login', title: "Login", component: LoginPageComponent},
  { path: 'register', title: "Register", component: RegisterPageComponent},
  { path: 'forum', title: "ForumPage", component: ForumPageComponent},
  { path: 'home', title: "Home", component: HomePageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
