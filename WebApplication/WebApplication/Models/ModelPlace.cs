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
        public virtual ModelUser Author { get; set; }
        public virtual ICollection<ModelPhoto> Photos { get; set; } = new HashSet<ModelPhoto>();

        public struct View
        {
            public int PlaceId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public double Rate { get; set; }
            public string Address { get; set; }
            public string Author { get; set; }
            public IEnumerable<ModelPhoto.View> Photos { get; set; }
        }

        public View GetView()
        {
            return new View
            {
                PlaceId = PlaceId,
                Address = Address,
                Author = Author.UserName,
                Description = Description,
                Name = Name,
                Photos = from photo in Photos select photo.GetView(),
                Rate = Rate
            };
        }
    }
}