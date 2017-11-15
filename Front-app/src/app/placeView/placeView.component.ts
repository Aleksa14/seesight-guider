import { Component } from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import { Place } from '../_models/index'
import { PlaceService, AlertService } from '../_services/index';

@Component({
    templateUrl: './placeView.component.html'
})

export class PlaceViewComponent {
    place: Place;


}
