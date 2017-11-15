import {Component} from '@angular/core';
import {Router} from '@angular/router';
import { AuthGuard } from '../_guards/index';

import 'rxjs/add/operator/filter';
import {User} from "../_models/user";

@Component({
    selector: 'bars',
    templateUrl: './navbar.component.html'
})

export class NavbarComponent {
    logged = false;
    user: User;

    constructor(){
       /* if (!this.isVisible()){
            this.user = JSON.parse(localStorage.getItem('currentUser'));
        }
        console.log(this.user);*/
    }

    public isVisible() {
        if (localStorage.getItem('currentUser')) {
            this.user = JSON.parse(localStorage.getItem('currentUser'));
            return false;
        }
        return true;
    }
}