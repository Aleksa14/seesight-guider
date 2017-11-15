"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/http");
var app_routing_1 = require("./app.routing");
var app_component_1 = require("./app.component");
var index_1 = require("./login/index");
var index_2 = require("./register/index");
var index_3 = require("./navbar/index");
var index_4 = require("./_directives/index");
var index_5 = require("./search/index");
var index_6 = require("./_guards/index");
var index_7 = require("./_services/index");
var addingPlace_component_1 = require("./addingPlace/addingPlace.component");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [platform_browser_1.BrowserModule,
            forms_1.FormsModule,
            forms_1.ReactiveFormsModule,
            http_1.HttpModule,
            app_routing_1.routing],
        declarations: [app_component_1.AppComponent,
            index_1.LoginComponent,
            index_2.RegisterComponent,
            index_3.NavbarComponent,
            index_4.AlertComponent,
            index_5.SearchComponent,
            addingPlace_component_1.AddingPlaceComponent],
        providers: [index_6.AuthGuard,
            index_7.AlertService,
            index_7.AuthenticationService,
            index_7.UserService,
            index_7.PlaceService],
        bootstrap: [app_component_1.AppComponent]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map