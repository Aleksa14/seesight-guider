"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
require("rxjs/add/operator/filter");
var NavbarComponent = (function () {
    function NavbarComponent() {
        this.logged = false;
        /* if (!this.isVisible()){
             this.user = JSON.parse(localStorage.getItem('currentUser'));
         }
         console.log(this.user);*/
    }
    NavbarComponent.prototype.isVisible = function () {
        if (localStorage.getItem('currentUser')) {
            this.user = JSON.parse(localStorage.getItem('currentUser'));
            return false;
        }
        return true;
    };
    return NavbarComponent;
}());
NavbarComponent = __decorate([
    core_1.Component({
        selector: 'bars',
        templateUrl: './navbar.component.html'
    }),
    __metadata("design:paramtypes", [])
], NavbarComponent);
exports.NavbarComponent = NavbarComponent;
//# sourceMappingURL=navbar.component.js.map