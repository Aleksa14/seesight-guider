import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AgmCoreModule } from '@agm/core';
import { StarRatingModule } from 'angular-star-rating';

import { routing } from './app.routing';
import { AppComponent }  from './app.component';
import { LoginComponent } from './login/index';
import { RegisterComponent } from './register/index';
import { NavbarComponent } from './navbar/index';
import { AlertComponent } from './_directives/index';
import { SearchComponent } from './search/index';
import { AuthGuard } from "./_guards/index";
import { UserService, AuthenticationService, AlertService, PlaceService } from "./_services/index";
import { AddingPlaceComponent } from "./addingPlace/index";
import { PlaceViewComponent } from "./placeView/index"


@NgModule({
  imports:      [ BrowserModule,
                  FormsModule,
                  ReactiveFormsModule,
                  HttpModule,
                  routing,
                  AgmCoreModule.forRoot({
                      apiKey: 'AIzaSyCpIT5UCiWv_vXt04wzBKcVrHfgPwEufZ8',
                      libraries: ["places"]
                  }),
                  StarRatingModule.forRoot()],
  declarations: [ AppComponent,
                  LoginComponent,
                  RegisterComponent,
                  NavbarComponent,
                  AlertComponent,
                  SearchComponent,
                  AddingPlaceComponent,
                  PlaceViewComponent],
  providers: [ AuthGuard,
                AlertService,
                AuthenticationService,
                UserService,
                PlaceService],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
