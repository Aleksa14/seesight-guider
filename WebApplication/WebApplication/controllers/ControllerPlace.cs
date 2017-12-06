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
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class ControllerPlace : NancyModule
    {
        private struct PostPlaceBody
        {
            public string Name;
            public string Description;
            public string Address;
            public double? Latitude;
            public double? Longitude;
        }

        private struct PostPhotoBody
        {
            public string Url;
        }

        private struct PostRateBody
        {
            public int Rate;
        }

        private struct CommentBody
        {
            public string Message;
        }

        public ControllerPlace()
        {
            Get["/api/places"] = GetPlaces;
            Get["/api/places/{id}"] = GetPlacesId;
            Get["/api/places/{id}/photos/{photoId}"] = GetPhotoById;
            Post["/api/places"] = PostPlace;
            Post["/api/places/{id}/photos"] = PostPhoto;
            Post["/api/places/{id}/rate"] = PostRate;
            Post["/api/places/{id}/comments"] = PostComment;
        }

        private dynamic PostComment(dynamic parametrs)
        {
            try
            {
                var db = new MainContext();
                var user = GetLoggedUser(db);
                var placeId = (int?) parametrs.id;
                if (placeId == null)
                {
                    return Response.AsJson("Wrong place id.", HttpStatusCode.BadRequest);
                }
                var body = this.Bind<CommentBody>();
                if (string.IsNullOrEmpty(body.Message))
                {
                    return Response.AsJson("Body not complete", HttpStatusCode.BadRequest);
                }
                ModelComment comment = ServicePlace.AddComment(placeId, user, body.Message, db);
                return Response.AsJson(comment.GetView());
            }
            catch (NooneLoggedInException)
            {
                return Response.AsJson("Noone logged in.", HttpStatusCode.Unauthorized);
            }
            catch (LoggedUserDoesNotExists)
            {
                return Response.AsJson("Logged user with bad userName", HttpStatusCode.InternalServerError);
            }
            catch (PlaceDoesNotExistsException)
            {
                return Response.AsJson("Place don't exists", HttpStatusCode.BadRequest);
            }
        }

        private dynamic PostRate(dynamic parameters)
        {
            try
            {
                var db = new MainContext();
                var user = GetLoggedUser(db);
                var placeId = (int?) parameters.id;
                if (placeId == null)
                {
                    return Response.AsJson("Wrong place id.", HttpStatusCode.BadRequest);
                }
                var rate = this.Bind<PostRateBody>().Rate;
                ServicePlace.RatePlace(placeId, user, rate, db);
                return Response.AsJson("Rated");
            }
            catch (InDataError)
            {
                return Response.AsJson("Internal server error", HttpStatusCode.InternalServerError);
            }
            catch (PlaceDoesNotExistsException)
            {
                return Response.AsJson("Place don't exists", HttpStatusCode.BadRequest);
            }
            catch (WrongDataException)
            {
                return Response.AsJson("Rate not in range [0, 5]", HttpStatusCode.BadRequest);
            }
            catch (NooneLoggedInException)
            {
                return Response.AsJson("Noone logged in.", HttpStatusCode.Unauthorized);
            }
            catch (LoggedUserDoesNotExists)
            {
                return Response.AsJson("Logged user with bad userName", HttpStatusCode.InternalServerError);
            }
        }

        private ModelUser GetLoggedUser(MainContext db)
        {
            var userName = (string) Request.Session[ControllerUser.SessionUserNameKey];
            if (string.IsNullOrEmpty(userName))
            {
                throw new NooneLoggedInException();
            }
            var user = ServiceUser.GetLoggedUser(userName, db);
            if (user == null)
            {
                throw new LoggedUserDoesNotExists();
            }
            return user;
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
                var placeId = (int) parameters.id;
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
            var name = (string) Request.Query.name;
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

        private dynamic PostPlace(dynamic parameters)
        {
            try
            {
                var userName = (string) this.Request.Session[ControllerUser.SessionUserNameKey];
                if (string.IsNullOrEmpty(userName))
                {
                    Console.WriteLine("No i dupa");
                    return Response.AsJson("Noone logged in.", HttpStatusCode.NotFound);
                }
                var db = new MainContext();
                var user = ServiceUser.GetLoggedUser(userName, db);
                if (user == null)
                {
                    return Response.AsJson("Logged user with bad userName", HttpStatusCode.InternalServerError);
                }
                var body = this.Bind<PostPlaceBody>();
                if (string.IsNullOrEmpty(body.Name) ||
                    string.IsNullOrEmpty(body.Description) ||
                    string.IsNullOrEmpty(body.Address) ||
                    body.Latitude == null ||
                    body.Longitude == null)
                {
                    return Response.AsJson("Body not completed.", HttpStatusCode.BadRequest);
                }
                var place = ServicePlace.CreatePlace(body.Name, body.Description, body.Address, (double) body.Latitude, (double) body.Longitude, user, db);
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

        private dynamic PostPhoto(dynamic parameters)
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
                var body = this.Bind<PostPhotoBody>();
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