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

        public static ModelPhoto AddPhoto(ModelPlace place, string url, MainContext db)
        {
            var photo = new ModelPhoto {Place = place, Url = url};
            db.Photos.Add(photo);
            place.Photos.Add(photo);
            db.SaveChanges();
            return photo;
        }
    }
}