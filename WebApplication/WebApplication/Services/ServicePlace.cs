using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Contexts;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class ServicePlace
    {
        public static ModelPlace CreatePlace(string name, string description, string address, ModelUser loggedUser)
        {
            using (var db = new MainContext())
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
        }

        public static IEnumerable<ModelPlace> GetAllPlaces()
        {
            using (var db = new MainContext())
            {
                return db.Places;
            }
        }

        public static IEnumerable<ModelPlace> GetAllPlacesMatchingName(string nameFragment)
        {
            return from place in GetAllPlaces() where place.Name.Contains(nameFragment) select place;
        }
    }
}