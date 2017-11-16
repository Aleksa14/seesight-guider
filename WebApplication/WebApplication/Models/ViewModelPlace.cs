using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ViewModelPlace
    {
        public ViewModelPlace(ModelPlace place)
        {
            this.PlaceId = place.PlaceId;
            this.Name = place.Name;
            this.Description = place.Description;
            this.Address = place.Address;
            this.Rate = place.Rate;
            this.Author = place.Author.UserName;
            this.PhotosId =
                from photo in place.Photos
                select photo.PhotoId;
        }

        public int PlaceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; } = .0;
        public string Address { get; set; }
        public string Author { get; set; }
        public IEnumerable<int> PhotosId { get; set; }
    }
}