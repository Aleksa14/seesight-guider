import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response, RequestMethod, Request } from '@angular/http';

import {Place} from "../_models/index";

@Injectable()
export class PlaceService {

    host = "http://localhost:56658/";

    constructor(private http: Http) { }

    getHeader() : Headers{
        let header = new Headers();
        header.append("Content-Type", "application/json");
        header.append("Accept", "application/json");
        return header;
    }

    add(place: Place) {
        let options = new RequestOptions({
            method: RequestMethod.Put,
            url: this.host ,
            headers: this.getHeader(),
            body: JSON.stringify(place)
        });
        return this.http.request(new Request(options));
    }

    getPlaces(word: String){
        var options = new RequestOptions({
            method: RequestMethod.Get,
            url: this.host,
            headers: this.getHeader()
        });
        return this.http.request(new Request(options))
            .map((response: Response) => response.json());
    }

    getPlace(id: number){
        var options = new RequestOptions({
            method: RequestMethod.Get,
            url: this.host,
            headers: this.getHeader()
        });
        return this.http.request(new Request(options))
            .map((response: Response) => response.json());
    }
}
