using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Nancy;
using WebApplication.Contexts;
using WebApplication.Exeptions;
using WebApplication.Migrations;
using WebApplication.Models;

namespace WebApplication.Service
{
    public class ServicePlace
    {
        public static ModelPlace CreatePlace(string name, string description, string address, ModelUser loggedUser,
            MainContext db)
        {
            var place = new ModelPlace
            {
                Name = name,
                Description = description,
                Address = address,
                Author = loggedUser
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
    }
}