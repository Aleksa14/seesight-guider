﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Contexts;
using WebApplication.Migrations;
using WebApplication.Models;

namespace WebApplication.Service
{ 
    public class ServicePlace
    {
        public static ModelPlace CreatePlace(string name, string description, string address, ModelUser loggedUser, MainContext db)
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

        public static IEnumerable<ModelPlace> GetAllPlaces(MainContext db)
        {
                return db.Places;
        }

        public static IEnumerable<ModelPlace> GetAllPlacesMatchingName(string nameFragment, MainContext db)
        {
            return from place in GetAllPlaces(db) where place.Name.Contains(nameFragment) select place;
        }


    }
}