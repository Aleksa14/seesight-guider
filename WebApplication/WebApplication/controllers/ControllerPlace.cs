using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;
using WebApplication.Contexts;
using WebApplication.Exeptions;
using WebApplication.Models;
using WebApplication.Service;
using WebApplication.Services;

namespace WebApplication.controllers
{
    public class ControllerPlace : NancyModule
    {
        private struct PutPlaceBody
        {
            public string Name;
            public string Description;
            public string Address;
        }
        
        public ControllerPlace()
        {
            Get["/api/places"] = GetPlaces;
            Put["/api/places"] = PutPlace;
            
        }

        private dynamic GetPlaces(dynamic parameters)
        {
            var name = (string) this.Request.Query.name;
            var db = new MainContext();
            if (string.IsNullOrEmpty(name))
            {
                return Response.AsJson(
                    from place in ServicePlace.GetAllPlaces(db)
                    select new ViewModelPlace(place));
            }
            return Response.AsJson(
                from place in ServicePlace.GetAllPlacesMatchingName(name, db)
                select new ViewModelPlace(place));
        }

        private dynamic PutPlace(dynamic parameters)
        {
            try
            {
                var userName = (string) this.Request.Session[UserController.SessionUserNameKey];
                if (string.IsNullOrEmpty(userName))
                {
                    return Response.AsJson("Noone logged in.", HttpStatusCode.Unauthorized);
                }
                var db = new MainContext();
                var user = ServiceUser.GetLoggedUser(userName, db);
                var body = this.Bind<PutPlaceBody>();
                if (string.IsNullOrEmpty(body.Name) ||
                    string.IsNullOrEmpty(body.Description) ||
                    string.IsNullOrEmpty(body.Address))
                {
                    return Response.AsJson("Body not completed.", HttpStatusCode.BadRequest);
                }
                var place = ServicePlace.CreatePlace(body.Name, body.Description, body.Address, user, db);
                return Response.AsJson(new ViewModelPlace(place));
            }
            catch (InDataError)
            {
                return HttpStatusCode.InternalServerError;
            }
            catch (ModelBindingException)
            {
                return Response.AsJson("Body not completed.", HttpStatusCode.BadRequest);
            }
        }
    }
}