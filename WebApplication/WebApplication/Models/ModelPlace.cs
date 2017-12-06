using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class ModelPlace
    {
        [Key]
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; } = .0;
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual ModelUser Author { get; set; }
        public virtual ICollection<ModelPhoto> Photos { get; set; } = new HashSet<ModelPhoto>();
        public virtual ICollection<ModelComment> Comments { get; set; } = new HashSet<ModelComment>();

        public struct View
        {
            public int PlaceId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public double Rate { get; set; }
            public string Address { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public string Author { get; set; }
            public IEnumerable<ModelPhoto.View> Photos { get; set; }
            public IEnumerable<ModelComment.View> Comments { get; set; }
        }

        public View GetView()
        {
            return new View
            {
                PlaceId = PlaceId,
                Address = Address,
                Latitude = Latitude,
                Longitude = Longitude,
                Author = Author.UserName,
                Description = Description,
                Name = Name,
                Photos = from photo in Photos select photo.GetView(),
                Rate = Rate,
                Comments = Comments.Select(place => place.GetView())
            };
        }

        public void UpdateRate(IEnumerable<ModelRate> rates)
        {
            var ratesList = rates as IList<ModelRate> ?? rates.ToList();
            if (!ratesList.Any()) return;
            var sum = 0;
            foreach (var rate in ratesList)
            {
                sum += rate.Rate;
            }
            Rate = (double) sum / ratesList.Count();
            System.Console.WriteLine("New rate " + Rate);
        }

        public override bool Equals(object obj)
        {
            if (obj is ModelPlace place)
            {
                return place.PlaceId == PlaceId;
            }
            return false;
        }
    }
}