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
var http_1 = require("@angular/http");
var UserService = (function () {
    function UserService(http) {
        this.http = http;
        this.host = "http://localhost:56658/";
    }
    UserService.prototype.getHeader = function () {
        var header = new http_1.Headers();
        header.append("Content-Type", "application/json");
        header.append("Accept", "application/json");
        return header;
    };
    UserService.prototype.create = function (user) {
        var options = new http_1.RequestOptions({
            method: http_1.RequestMethod.Post,
            url: this.host + 'api/users/' + user.username,
            headers: this.getHeader(),
            body: JSON.stringify({ password: user.password, email: user.email })
        });
        return this.http.request(new http_1.Request(options));
    };
    return UserService;
}());
UserService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], UserService);
exports.UserService = UserService;
//# sourceMappingURL=user.service.js.map