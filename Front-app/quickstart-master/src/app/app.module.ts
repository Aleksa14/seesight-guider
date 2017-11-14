import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { routing } from './app.routing';
import { AppComponent }  from './app.component';
import { LoginComponent } from './login/index';
import { RegisterComponent } from './register/index';
import { NavbarComponent } from './navbar/index';
import { AlertComponent } from './_directives/index';
import { AuthGuard } from "./_guards/index";
import { UserService, AuthenticationService, AlertService } from "./_services/index";

@NgModule({
  imports:      [ BrowserModule,
                  FormsModule,
                  ReactiveFormsModule,
                  HttpModule,
                  routing ],
  declarations: [ AppComponent,
                  LoginComponent,
                  RegisterComponent,
                  NavbarComponent,
                  AlertComponent ],
  providers: [ AuthGuard,
                AlertService,
                AuthenticationService,
                UserService ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
