"use strict";
var router_1 = require("@angular/router");
var index_1 = require("./login/index");
var index_2 = require("./register/index");
var search_component_1 = require("./search/search.component");
var addingPlace_component_1 = require("./addingPlace/addingPlace.component");
var appRoutes = [
    //{ path: '', component: t, canActivate: [AuthGuard] },
    { path: 'login', component: index_1.LoginComponent },
    { path: 'register', component: index_2.RegisterComponent },
    { path: 'searching', component: search_component_1.SearchComponent },
    { path: 'adding', component: addingPlace_component_1.AddingPlaceComponent },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routing.js.map