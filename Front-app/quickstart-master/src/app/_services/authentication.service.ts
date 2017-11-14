import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions, RequestMethod, Request } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'

@Injectable()
export class AuthenticationService {

    //host = " ";

    constructor(private http: Http) { }

    login(username: string, password: string) {
        var header = new Headers();
        header.append("Content-Type", "application/json");
        header.append("Accept", "application/json");
        var option = new RequestOptions({
            method: RequestMethod.Post,
            url: /*this.host +*/ '/user-auth',
            headers: header,
            body: JSON.stringify({username: username, password: password}),
        });
        return this.http.request(new Request(option))
            .map((response: Response) => {
                // login successful if there's a jwt token in the response
                let user = response.json();
                if (user) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }

                return user;
            });
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }
}