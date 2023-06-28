import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FeedPageComponent } from './feed-page/feed-page.component';
import { PostUserComponent } from './post-user/post-user.component';
import { MatButtonModule } from '@angular/material/button';
import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { LoginPageComponent } from './login-page/login-page.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { NgIf } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { SideBarComponent } from './side-bar/side-bar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { RegisterPageComponent } from './register-page/register-page.component';
import { CreatePostComponent } from './create-post/create-post.component';
import {MatSelectModule} from '@angular/material/select';
import {MatMenuModule} from '@angular/material/menu';
import { CreatePasswordComponent } from './create-password/create-password.component';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { HttpClientModule } from '@angular/common/http';
import { ForumPageComponent } from './forum-page/forum-page.component';
import { StaticBarComponent } from './static-bar/static-bar.component';

@NgModule({
    declarations: [
        AppComponent,
        PostUserComponent,
        FeedPageComponent,
        LoginPageComponent,
        NavComponent,
        SideBarComponent,
        RegisterPageComponent,
        CreatePostComponent,
        CreatePasswordComponent,
        ForumPageComponent,
        StaticBarComponent
    ],
    providers: [
    ],
    bootstrap: [AppComponent],
    imports: [
        MatProgressBarModule,
        MatMenuModule,
        MatSelectModule,
        MatFormFieldModule,
        MatInputModule,
        NgIf,
        MatNativeDateModule,
        MatDatepickerModule,
        MatCardModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserModule,
        AppRoutingModule,
        MatToolbarModule,
        MatIconModule,
        BrowserAnimationsModule,
        MatButtonModule,
        MatCardModule,
        HttpClientModule
    ]
})
export class AppModule { }
