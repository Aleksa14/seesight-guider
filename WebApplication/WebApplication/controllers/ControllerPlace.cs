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

namespace WebApplication.Controllers
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
            Get["/api/places/{id}"] = GetPlacesId;
            Put["/api/places"] = PutPlace;
            
        }

        private dynamic GetPlacesId(dynamic parameters)
        {
            try
            {
                var placeId = (int) parameters.id;
                var db = new MainContext();
                var place = ServicePlace.GetPlaceById(placeId, db);
                return place == null ? Response.AsJson("There is no such place.", HttpStatusCode.NotFound) : Response.AsJson(new ViewModelPlace(place));
            }
            catch (InDataError)
            {
                return Response.AsJson("Internal server error", HttpStatusCode.InternalServerError);
            }
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
                Console.WriteLine("Jeszcze nie dupa");
                var userName = (string) this.Request.Session[ControllerUser.SessionUserNameKey];
                if (string.IsNullOrEmpty(userName))
                {
                    Console.WriteLine("No i dupa");
                    return Response.AsJson("Noone logged in.", HttpStatusCode.NotFound);
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