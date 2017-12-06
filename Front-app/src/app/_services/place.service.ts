import { Injectable } from '@angular/core';
import {Http, Headers, RequestOptions, Response, RequestMethod, Request, URLSearchParams} from '@angular/http';

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
            url: this.host + 'api/places',
            headers: this.getHeader(),
            body: JSON.stringify(place),
            withCredentials: true
        });
        return this.http.request(new Request(options))
            .map((response: Response) => response.json());
    }

    getPlaces(word: string){
        let params: URLSearchParams = new URLSearchParams();
        params.set("name", word);
        var options = new RequestOptions({
            method: RequestMethod.Get,
            url: this.host + 'api/places',
            headers: this.getHeader(),
            search: params,
            withCredentials: true
        });
        return this.http.request(new Request(options))
            .map((response: Response) => response.json());
    }

    getPlace(id: number){

        var options = new RequestOptions({
            method: RequestMethod.Get,
            url: this.host + 'api/places/' + id,
            headers: this.getHeader(),
            withCredentials: true
        });
        return this.http.request(new Request(options))
            .map((response: Response) => response.json());
    }
}
