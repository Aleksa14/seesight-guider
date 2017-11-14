"use strict";
var router_1 = require("@angular/router");
var index_1 = require("./login/index");
var index_2 = require("./register/index");
var appRoutes = [
    //{ path: '', component: t, canActivate: [AuthGuard] },
    { path: 'login', component: index_1.LoginComponent },
    { path: 'register', component: index_2.RegisterComponent },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routing.js.map