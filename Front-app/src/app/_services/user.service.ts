import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response, RequestMethod, Request } from '@angular/http';

import { User } from '../_models/index';

@Injectable()
export class UserService {

    //host = "";

    constructor(private http: Http) { }

    getHeader() : Headers{
        let header = new Headers();
        header.append("Content-Type", "application/json");
        return header;
    }

    create(user: User) {
        let options = new RequestOptions({
            method: RequestMethod.Post,
            url: /*this.host +*/ 'users/' + user.username,
            headers: this.getHeader(),
            body: JSON.stringify(user)
        });
        return this.http.request(new Request(options)).map((response: Response) => response.json());
    }
}