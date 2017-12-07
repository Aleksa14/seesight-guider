import {Component, OnInit} from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import { Place } from '../_models/index'
import { PlaceService, AlertService } from '../_services/index';
import {OnClickEvent, OnRatingChangeEven} from "angular-star-rating";

@Component({
    templateUrl: './placeView.component.html'
})

export class PlaceViewComponent {

    place: Place;
    id: number;
    public zoom: number;
    onClickResult:OnClickEvent;
    onRatingChangeResult:OnRatingChangeEven;
    comment: string;

    constructor(private placeService: PlaceService,
                private alertService: AlertService,
                private activatedRoute: ActivatedRoute,
                private router: Router){
        this.id = activatedRoute.snapshot.params['placeId'];
        this.getPlace();
    }

    getPlace(): void{
        this.placeService.getPlace(this.id).subscribe(
            (place) => {
                this.place = place;
                //this.latitude = (google.maps.places.PlaceResult) this.place.address.ge
                this.zoom = 14;
            },
            error => {
                this.alertService.error(error.message);
                this.router.navigate(['/searching']);
            }
        )
    }


    onClick = ($event:OnClickEvent) => {
        this.place.rate = $event.rating;
        this.placeService.postRate(this.id, this.place.rate).subscribe(
            (data) => {
                this.getPlace();
            }
        )
    };

    addComment(){
        this.placeService.addComment(this.id, this.comment).subscribe(
            (data) => {
                this.getPlace();
            }
        )
    }

}
