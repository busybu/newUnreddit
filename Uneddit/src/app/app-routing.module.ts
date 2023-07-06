import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeedPageComponent } from './feed-page/feed-page.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { ForumPageComponent } from './forum-page/forum-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { UserPageComponent } from './user-page/user-page.component';
import { ListForumsComponent } from './list-forums/list-forums.component';
import { NotFoundComponent } from './not-found/not-found.component';

const routes: Routes = [
  { path: '', title: "Feed", component: FeedPageComponent },
  { path: 'login', title: "Login", component: LoginPageComponent},
  { path: 'register', title: "Register", component: RegisterPageComponent},
  { path: 'forum', title: "ForumPage", component: ForumPageComponent},
  { path: 'home', title: "Home", component: HomePageComponent},
  { path: 'user', title: "User", component: UserPageComponent},
  { path: 'forum/:title', title: "Forum", component: ForumPageComponent},
  { path: '**', title: "Not Found", component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
