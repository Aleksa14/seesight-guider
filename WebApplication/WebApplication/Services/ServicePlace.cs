using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Contexts;
using WebApplication.Exeptions;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class ServicePlace
    {
        public static ModelPlace CreatePlace(string name, string description, string address, double latitude,
            double longitude, ModelUser loggedUser,
            MainContext db)
        {
            var place = new ModelPlace
            {
                Name = name,
                Description = description,
                Address = address,
                Author = loggedUser,
                Latitude = latitude,
                Longitude = longitude
            };
            loggedUser.OwnedPlaces.Add(place);
            db.Places.Add(place);
            db.SaveChanges();
            return place;
        }

        public static ModelPlace GetPlaceById(int? id, MainContext db)
        {
            var places =
                from place in db.Places
                where place.PlaceId == id
                select place;
            if (!places.Any())
            {
                return null;
            }
            if (places.Count() > 1)
            {
                throw new InDataError();
            }
            return places.First();
        }

        public static IEnumerable<ModelPlace> GetAllPlaces(MainContext db)
        {
            return db.Places;
        }

        public static IEnumerable<ModelPlace> GetAllPlacesMatchingName(string nameFragment, MainContext db)
        {
            return from place in GetAllPlaces(db) where place.Name.Contains(nameFragment) select place;
        }

        public static ModelPhoto AddPhoto(ModelPlace place, ModelUser user, string url, MainContext db)
        {
            if (!user.Equals(place.Author))
            {
                throw new UnauthorizedAccessException();
            }
            var photo = new ModelPhoto {Place = place, Url = url};
            db.Photos.Add(photo);
            place.Photos.Add(photo);
            db.SaveChanges();
            return photo;
        }

        public static ModelPhoto GetPhotoById(int? placeId, int? photoId, MainContext db)
        {
            ModelPlace place = GetPlaceById(placeId, db);
            if (place == null)
            {
                return null;
            }
            var photos = from modelPhoto in db.Photos where modelPhoto.PhotoId == photoId select modelPhoto;
            if (!photos.Any())
            {
                return null;
            }
            if (photos.Count() > 1)
            {
                throw new InDataError();
            }
            if (!place.Photos.Contains(photos.First()))
            {
                throw new NotContaining();
            }
            return photos.First();
        }

        public static void RatePlace(int? placeId, ModelUser user, int rate, MainContext db)
        {
            if (rate < 0 || rate > 5)
            {
                throw new WrongDataException();
            }
            var place = GetPlaceById(placeId, db);
            if (place == null)
            {
                throw new PlaceDoesNotExistsException();
            }
            var placeRates = from rateModel in user.Rates
                where place.PlaceId == rateModel.RatedPlace.PlaceId
                select rateModel;
            var placeRatesList = placeRates as IList<ModelRate> ?? placeRates.ToList();
            if (!placeRatesList.Any())
            {
                var newRate = new ModelRate {Rate = rate, RatedPlace = place};
                db.Rates.Add(newRate);
                user.Rates.Add(newRate);
            }
            else
            {
                if (placeRatesList.Count == 1)
                {
                    placeRatesList.First().Rate = rate;
                }
                else
                {
                    throw new InDataError();
                }
            }
            place.UpdateRate(db.Rates.Where(rateModel => place.PlaceId == rateModel.RatedPlace.PlaceId));
            db.SaveChanges();
        }

        public static ModelComment AddComment(int? placeId, ModelUser user, string message, MainContext db)
        {
            var place = GetPlaceById(placeId, db);
            if (place == null)
            {
                throw new PlaceDoesNotExistsException();
            }
            ModelComment comment = new ModelComment {Author = user, CommentMessage = message, Place = place};
            db.Comments.Add(comment);
            place.Comments.Add(comment);
            db.SaveChanges();
            return comment;
        }
    }
}