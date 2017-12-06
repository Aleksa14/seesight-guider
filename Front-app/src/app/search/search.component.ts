import { Component } from '@angular/core';
import { Place } from '../_models/index'
import { PlaceService, AlertService } from '../_services/index';

@Component({
    templateUrl: './search.component.html'
})

export class SearchComponent {

    searched: boolean;
    placeTable: Place[];
    word: string;

    constructor(
        private placeService: PlaceService,
        private alertService: AlertService) {
        this.searched = false;
    }

    search(){
        this.searched = true;
        this.placeService.getPlaces(this.word).subscribe(
            (placeTable) => { this.placeTable = placeTable; },
            error => { this.alertService.error(error.message);}
        )
    }

}
