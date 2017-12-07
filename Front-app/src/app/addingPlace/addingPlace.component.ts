import {Component, ElementRef, ViewChild, NgZone, OnInit} from '@angular/core';
import { Router } from '@angular/router';

import { PlaceService, AlertService } from '../_services/index';
import {MapsAPILoader} from "@agm/core";
import {FormControl, FormGroup, Validators, FormArray} from "@angular/forms";

@Component({
    templateUrl: './addingPlace.component.html'
})

export class AddingPlaceComponent implements OnInit{
    model: any = {};
    loading = false;
    public searchControl: FormControl;

    form = new FormGroup({
        name: new FormControl('', Validators.required),
        description: new FormControl('', Validators.required),
        search: new FormControl('', Validators.required),
        photos: new FormArray([
            new FormControl('', Validators.required)
        ], Validators.required)
    });

    @ViewChild("search")
    public searchElementRef: ElementRef;

    constructor(
        private mapsAPILoader: MapsAPILoader,
        private ngZone: NgZone,
        private router: Router,
        private placeService: PlaceService,
        private alertService: AlertService) { }

    ngOnInit(): void{
        this.searchControl = new FormControl();

        this.mapsAPILoader.load().then(() => {
            let autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement, {
                types: ["address"]
            });
            autocomplete.addListener("place_changed", () => {
                this.ngZone.run(() => {
                    //get the place result
                    let place: google.maps.places.PlaceResult = autocomplete.getPlace();
                    console.log("place from google maps "+place.formatted_address);
                    //verify result
                    if (place.geometry === undefined || place.geometry === null) {
                        return;
                    }
                    this.model.address = place.formatted_address;
                    this.model.latitude = place.geometry.location.lat();
                    this.model.longitude = place.geometry.location.lng();
                });
            });
        });

    }

    get photos(): FormArray{
        return this.form.get('photos') as FormArray;
    }

    addPhoto(){
        this.photos.push(new FormGroup({
            photo: new FormControl('', Validators.required)
        }));
    }

    add(){
        this.model.name = this.form.controls['name'].value;
        this.model.description = this.form.controls['description'].value;
        this.model.photos = this.form.controls['photos'].value;
        console.log("adding place: " + JSON.stringify(this.model));
        console.log("place: " + this.model.address + this.model.latitude + this.model.longitude);
        this.loading = true;
        this.placeService.add(this.model).subscribe(
            data => {
                this.alertService.success('Place was added successfully', true);
                this.router.navigate(['/searching']);
            },
            error => {
                this.alertService.error(error.message);
                this.loading = false;
            }
        )
    }
}
