import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login/index';
import { RegisterComponent } from './register/index';
import { AuthGuard } from './_guards/index';
import {SearchComponent} from "./search/search.component";
import {AddingPlaceComponent} from "./addingPlace/addingPlace.component";

const appRoutes: Routes = [
    //{ path: '', component: t, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'searching', component: SearchComponent},
    { path: 'adding', component: AddingPlaceComponent},

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);
