import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response, RequestMethod, Request } from '@angular/http';

import { RegistrationUser } from '../_models/index';

@Injectable()
export class UserService {

    host = "http://localhost:56658/";

    constructor(private http: Http) { }

    getHeader() : Headers{
        let header = new Headers();
        header.append("Content-Type", "application/json");
        header.append("Accept", "application/json");
        return header;
    }

    create(user: RegistrationUser) {
        let options = new RequestOptions({
            method: RequestMethod.Post,
            url: this.host + 'api/users/' + user.username,
            headers: this.getHeader(),
            body: JSON.stringify({password: user.password, email: user.email})
        });
        return this.http.request(new Request(options));
    }
}