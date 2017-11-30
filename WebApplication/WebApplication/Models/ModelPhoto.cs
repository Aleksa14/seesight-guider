using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ModelPhoto
    {
        [Key]
        public int PhotoId { get; set; }

        public string Url { get; set; }
        public virtual ModelPlace Place { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ModelPhoto photo)
            {
                return photo.PhotoId == PhotoId;
            }
            return false;
        }

        public struct View
        {
            public int PhotoId { get; set; }
            public int PlaceId { get; set; }
            public string Url { get; set; }
        }

        public View GetView()
        {
            return new View {PhotoId = PhotoId, PlaceId = Place.PlaceId, Url = Url};
        }


    }
}