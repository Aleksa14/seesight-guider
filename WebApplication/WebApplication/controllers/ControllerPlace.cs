using System;
using System.Collections.Generic;
using System.IO;
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

        private struct PutPhotoBody
        {
            public string Url;
        }

        public ControllerPlace()
        {
            Get["/api/places"] = GetPlaces;
            Get["/api/places/{id}"] = GetPlacesId;
            Get["/api/places/{id}/photos/{photoId}"] = GetPhotoById;
            Put["/api/places/{id}/photos"] = PutPhoto;
            Put["/api/places"] = PutPlace;
        }

        private dynamic GetPhotoById(dynamic parameters)
        {
            try
            {
                var placeId = (int?) parameters.id;
                if (placeId == null)
                {
                    return Response.AsJson("Wrong place id.", HttpStatusCode.BadRequest);
                }
                var photoId = (int?) parameters.photoId;
                if (photoId == null)
                {
                    return Response.AsJson("Wrong photo id.", HttpStatusCode.BadRequest);
                }
                var db = new MainContext();
                ModelPhoto photo = ServicePlace.GetPhotoById(placeId, photoId, db);
                if (photo == null)
                {
                    return Response.AsJson("Photo or place does not exist.", HttpStatusCode.BadRequest);
                }
                return Response.AsJson(photo.GetView());

            }
            catch (InDataError)
            {
                return Response.AsJson("Internal server error", HttpStatusCode.InternalServerError);
            }
            catch (NotContaining)
            {
                return Response.AsJson("Photo not in place", HttpStatusCode.BadRequest);
            }
        }

        private dynamic GetPlacesId(dynamic parameters)
        {
            try
            {
                var placeId = (int?) parameters.id;
                if (placeId == null)
                {
                    return Response.AsJson("Body not completed.", HttpStatusCode.BadRequest);
                }
                var db = new MainContext();
                var place = ServicePlace.GetPlaceById(placeId, db);
                return place == null
                    ? Response.AsJson("There is no such place.", HttpStatusCode.NotFound)
                    : Response.AsJson(place.GetView());
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
                    select place.GetView());
            }
            return Response.AsJson(
                from place in ServicePlace.GetAllPlacesMatchingName(name, db)
                select place.GetView());
        }

        private dynamic PutPlace(dynamic parameters)
        {
            try
            {
                var userName = (string) this.Request.Session[ControllerUser.SessionUserNameKey];
                if (string.IsNullOrEmpty(userName))
                {
                    return Response.AsJson("Noone logged in.", HttpStatusCode.Unauthorized);
                }
                var db = new MainContext();
                var user = ServiceUser.GetLoggedUser(userName, db);
                if (user == null)
                {
                    return Response.AsJson("Logged user with bad userName", HttpStatusCode.InternalServerError);
                }
                var body = this.Bind<PutPlaceBody>();
                if (string.IsNullOrEmpty(body.Name) ||
                    string.IsNullOrEmpty(body.Description) ||
                    string.IsNullOrEmpty(body.Address))
                {
                    return Response.AsJson("Body not completed.", HttpStatusCode.BadRequest);
                }
                var place = ServicePlace.CreatePlace(body.Name, body.Description, body.Address, user, db);
                return Response.AsJson(place.GetView());
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

        private dynamic PutPhoto(dynamic parameters)
        {
            try
            {
                var userName = (string)Request.Session[ControllerUser.SessionUserNameKey];
                if (string.IsNullOrEmpty(userName))
                {
                    return Response.AsJson("Noone logged in.", HttpStatusCode.Unauthorized);
                }
                var db = new MainContext();
                var user = ServiceUser.GetLoggedUser(userName, db);
                if (user == null)
                {
                    return Response.AsJson("Logged user with bad userName", HttpStatusCode.InternalServerError);
                }
                var body = this.Bind<PutPhotoBody>();
                if (string.IsNullOrEmpty(body.Url))
                {
                    return Response.AsJson("Body not completed.", HttpStatusCode.BadRequest);
                }
                var placeId = parameters.id;
                var place = ServicePlace.GetPlaceById(placeId, db);
                if (place == null)
                {
                    return Response.AsJson("Place with this id not found.", HttpStatusCode.NotFound);
                }
                ModelPhoto photo = ServicePlace.AddPhoto(place, user, body.Url, db);
                return Response.AsJson(photo.GetView());
            }
            catch (InDataError)
            {
                return HttpStatusCode.InternalServerError;
            }
            catch (ModelBindingException)
            {
                return Response.AsJson("Body not completed.", HttpStatusCode.BadRequest);
            }
            catch (UnauthorizedAccessException)
            {
                return Response.AsJson("You are not author of this place.", HttpStatusCode.Unauthorized);
            }
        }
    }
}