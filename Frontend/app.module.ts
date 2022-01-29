import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CursuriComponent } from './cursuri/cursuri.component';
import { CourseDetailComponent } from './course-detail/course-detail.component';
import { MessagesComponent } from './messages/messages.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { FormTestComponent } from './form-test/form-test.component';
import { TemplateDrivenTestComponent } from './template-driven-test/template-driven-test.component';
import { HighlightDirective } from './directives/highlight.directive';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { JwtModule } from '@auth0/angular-jwt';
import { HeroChildComponent } from './hero-child/hero-child.component';
import { HeroParentComponent } from './hero-parent/hero-parent.component';
import { NameChildComponent } from './name-child/name-child.component';
import { NameParentComponent } from './name-parent/name-parent.component';
import { VoterComponent } from './voter/voter.component';
import { VotetakerComponent } from './votetaker/votetaker.component';
import { MissionControlComponent } from './mission-control/mission-control.component';
import { AstronautComponent } from './astronaut/astronaut.component';

@NgModule({
  declarations: [
    AppComponent,
    CursuriComponent,
    CourseDetailComponent,
    MessagesComponent,
    DashboardComponent,
    FormTestComponent,
    TemplateDrivenTestComponent,
    HighlightDirective,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    HeroChildComponent,
    HeroParentComponent,
    NameChildComponent,
    NameParentComponent,
    VoterComponent,
    VotetakerComponent,
    MissionControlComponent,
    AstronautComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter :() => {
          return localStorage.getItem("token");
        }
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
