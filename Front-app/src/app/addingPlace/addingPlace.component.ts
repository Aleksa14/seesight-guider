import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { PlaceService, AlertService } from '../_services/index';
import { Place, User } from "../_models/index";

@Component({
    templateUrl: './addingPlace.component.html'
})

export class AddingPlaceComponent {
    model: any = {};
    loading = false;

    constructor(
        private router: Router,
        private placeService: PlaceService,
        private alertService: AlertService) { }

    add(){
        this.loading = true;
        this.placeService.add(this.model).subscribe(
            data => {
                this.alertService.success('Place was added successfully', true);
                this.router.navigate(['/searching']);
            },
            error => {
                this.alertService.error(error.json.message);
                this.loading = false;
            }
        )
    }
}
