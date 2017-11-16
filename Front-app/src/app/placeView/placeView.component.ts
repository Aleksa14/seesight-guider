import {Component, OnInit} from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import { Place } from '../_models/index'
import { PlaceService, AlertService } from '../_services/index';

@Component({
    templateUrl: './placeView.component.html'
})

export class PlaceViewComponent implements OnInit{

    place: Place;
    id: number;

    constructor(private placeService: PlaceService,
                private alertService: AlertService,
                private activatedRoute: ActivatedRoute,
                private router: Router){
        this.id = activatedRoute.snapshot.params['placeId'];
        this.placeService.getPlace(this.id).subscribe(
            (place) => {
                this.place = place;
            },
            error => {
                this.alertService.error(error.message);
                this.router.navigate(['/searching']);
            }
        )
    }

    ngOnInit(): void{

    }




}
